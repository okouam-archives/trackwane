#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.OctoTools
open Fake.Testing
open System
open Fake.Paket
open Fake.FileUtils

let buildDir = "./.build/"
let installationDir  = "./.local/"
let serviceName = "Trackwane.Access.Control"
let executable = installationDir + "/Trackwane.AccessControl.Standalone.exe"

MSBuildDefaults <- { MSBuildDefaults with Verbosity = Some MSBuildVerbosity.Quiet }

Target "Clean" (fun _ ->
  CleanDir buildDir
)

Target "Compile" (fun _ ->
  !! "**/*.csproj"
    |> MSBuildDebug buildDir "Build"
    |> ignore
)

(*
Target "Run_Tests" (fun _ ->
  !! (testDir + "/Trackwane.AccessControl.Tests.dll")
    |> NUnit3 (fun p ->
      {p with
        ToolPath = "./packages/NUnit.Console/tools/nunit3-console.exe"
      })
)
*)
Target "Stop_Local_Service" (fun _ ->
  if checkServiceExists(serviceName) then
    StopService serviceName
    ensureServiceHasStopped serviceName (TimeSpan.FromMinutes 1.0)
)


Target "Uninstall_Local_Service" (fun _ ->
  let fileExists = TestFile(executable)
  if fileExists then Shell.Exec(executable, "uninstall") |> ignore
)

Target "Copy_To_Local_Service" (fun _ ->
  cp_r buildDir installationDir
)

Target "Install_Local_Service" (fun _ ->
  Shell.Exec(executable, "install") |> ignore
)

Target "Start_Local_Service" (fun _ ->
  startService serviceName
  ensureServiceHasStarted serviceName (TimeSpan.FromMinutes 1.0) |> ignore
)

Target "Deploy" DoNothing

Target "Install" DoNothing

"Clean"
  ==> "Compile"

(*
"Compile"
  ==> "Run_Tests"
*)

"Stop_Local_Service"
  ==> "Uninstall_Local_Service"
  ==> "Compile"
  ==> "Copy_To_Local_Service"
  ==> "Install_Local_Service"
  ==> "Start_Local_Service"
  ==> "Install"

RunTargetOrDefault "Compile"
