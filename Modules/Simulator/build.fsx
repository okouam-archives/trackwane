#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.OctoTools
open Fake.Testing
open System
open Fake.Paket
open Fake.FileUtils

let buildDir = "./.build/"
let installationDir  = "./.local/"
let serviceName = "Trackwane.Simulator"
let executable = installationDir + "/" + serviceName + ".Standalone.exe"

MSBuildDefaults <- { MSBuildDefaults with Verbosity = Some MSBuildVerbosity.Quiet }

Target "Clean" (fun _ ->
  CleanDir buildDir
)

Target "Compile" (fun _ ->
  !! "**/*.csproj"
    |> MSBuildDebug buildDir "Build"
    |> ignore
)

Target "Test" (fun _ ->
  !! (buildDir + "/" + serviceName + ".Tests.dll")
    |> NUnit3 (fun p ->
      {p with
        ToolPath = "./packages/NUnit.Console/tools/nunit3-console.exe"
      })
)

Target "Stop_Local_Service" (fun _ ->
  if checkServiceExists(serviceName) then
    StopService serviceName
    ensureServiceHasStopped serviceName (TimeSpan.FromMinutes 1.0)
)


Target "Uninstall" (fun _ ->
  let fileExists = TestFile(executable)
  if fileExists then Shell.Exec(executable, "uninstall") |> ignore
  rm_rf installationDir
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


"Compile"
  ==> "Test"

"Stop_Local_Service"
  ==> "Uninstall"
  ==> "Compile"
  ==> "Copy_To_Local_Service"
  ==> "Install_Local_Service"
  ==> "Start_Local_Service"
  ==> "Install"

RunTargetOrDefault "Compile"
