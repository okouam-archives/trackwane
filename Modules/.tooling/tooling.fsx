#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.OctoTools
open Fake.Testing
open System
open Fake.Paket
open Fake.FileUtils
open Fake.PaketTemplate
open Fake.AssemblyInfoFile
open Fake.Git

let buildDir = "./.build/"
let installationDir  = "./.local/"
let distDir  = "./.dist/"
let nugetApiKey = "API-MCZZVTPXAJ6XV2VSHYUMPCLOGN8"
let nugetPublishUrl = "http://octopus.wylesight.ws"
let nugetEndpoint = "nuget/packages"
let moduleName = getBuildParam "module"
let serviceName = "Trackwane." + moduleName
let executable = installationDir + "/" + serviceName + ".Standalone.exe"

printfn "====================================================================="
printfn "Running FAKE tools for %s" serviceName
printfn "====================================================================="

MSBuildDefaults <- { MSBuildDefaults with Verbosity = Some MSBuildVerbosity.Quiet }

Target "Clean" (fun _ ->
  CleanDir buildDir
)

Target "Compile" (fun _ ->
  !! "**/*.csproj"
    |> MSBuildDebug buildDir "Build"
    |> ignore
)

Target "Deploy" (fun _ ->
  Fake.Paket.Push (fun p ->
    {p with ApiKey = nugetApiKey; PublishUrl = nugetPublishUrl; EndPoint = nugetEndpoint; WorkingDir = distDir})
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

Target "Package" (fun _ ->
  MSBuildDebug null "Build" ["Standalone/Standalone.csproj"]
    |> ignore
  rm_rf distDir
  let version = GetAttributeValue "AssemblyVersion" "Version.cs"
  Shell.Exec(".paket\paket.exe", "pack output " + distDir + " templatefile paket.template version " + version.Value) |> ignore
)

Target "UndoVersioning" DoNothing

Target "Build" DoNothing

Target "Version" (fun _ ->
  trace (describe ".")
)

Target "Install" DoNothing

"Package"
  ==> "Deploy"

"Clean"
  ==> "UndoVersioning"
  ==> "Compile"
  ==> "Version"
  ==> "Build"

"Compile"
  ==> "Test"

"Stop_Local_Service"
  ==> "Uninstall"
  ==> "Compile"
  ==> "Copy_To_Local_Service"
  ==> "Install_Local_Service"
  ==> "Start_Local_Service"
  ==> "Install"

RunTargetOrDefault "Build"
