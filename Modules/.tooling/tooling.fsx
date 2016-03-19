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

// The output directory for building the module projects
let buildDir = "./.build/"

// The directory where the module Windows Service will be placed
let installationDir  = "./.local/"

// The directory where the NuGet package for the module Windows Service will be placed
let distDir  = "./.dist/"

// The API key for accessing Octopus Deploy
let nugetApiKey = "API-MCZZVTPXAJ6XV2VSHYUMPCLOGN8"

// The location of the Octopus Deploy server
let nugetPublishUrl = "http://octopus.wylesight.ws"

// The endpoint for pushing up packages to the Octopus Deploy server
let nugetEndpoint = "nuget/packages"

// Miscellaneous variables
let moduleName = getBuildParam "module"
let moduleVersion = getBuildParam "version"
let serviceName = "Trackwane." + moduleName
let executable = installationDir + "/" + serviceName + ".Standalone.exe"

printfn "====================================================================="
printfn "Running FAKE tools for %s (%s)" serviceName moduleVersion
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

Target "UndoVersion" (fun _ ->
  let description = GetAttributeValue "AssemblyDescription" "./Standalone/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo "./Standalone/Properties/AssemblyInfo.cs" [Attribute.Description description.Value.Replace("\"", "")]
)

Target "Build" DoNothing

Target "Version" (fun _ ->
  let description = GetAttributeValue "AssemblyDescription" "./Standalone/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo "./Standalone/Properties/AssemblyInfo.cs"
    [Attribute.Title serviceName;
     Attribute.Description description.Value.Replace("\"", "");
  	 Attribute.Product serviceName;
  	 Attribute.Version moduleVersion;
  	 Attribute.FileVersion moduleVersion]
)

Target "Install" DoNothing

"Package"
  ==> "Deploy"

"Clean"
  ==> "Version"
  ==> "Compile"
  ==> "UndoVersion"
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
