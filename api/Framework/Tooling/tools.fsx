#r "../packages/FAKE/tools/FakeLib.dll"
#load "./Targets/Building.fsx"
#load "./Targets/Packaging.fsx"
#load "./Targets/Services.fsx"
open Building
open Packaging
open Services
open Fake
open Fake.FileSystemHelper

//--> Setup

MSBuildDefaults <- { MSBuildDefaults with Verbosity = Some MSBuildVerbosity.Quiet }

let build_directory = getBuildParamOrDefault "build_directory" "./.build/"

let module_name = getBuildParam "component"

let distribution_directory = "./.dist/"

let service_name = "Trackwane." + module_name

let module_version = getBuildParam "version"
    
let nugetApiKey = "API-MCZZVTPXAJ6XV2VSHYUMPCLOGN8"

let nuget_publish_url = "http://octopus.wylesight.ws"

let nuget_endpoint = "nuget/packages"

let GetInstallationDirectory = 
    if hasBuildParam "installation_directory" 
    then getBuildParam "installation_directory"
    else ProgramFiles + "/Trackwane/" + module_name 

let GetExecutableName = 
    GetInstallationDirectory + "/" + service_name + ".Standalone.exe"


//--> Targets

Description "INTERNAL"
Target "Compile" (fun _ -> Compile module_name build_directory)

Description "INTERNAL"
Target "Clean" (fun _ -> Clean build_directory)

Description "INTERNAL"
Target "Version" (fun _ -> Version service_name module_name)

Description "INTERNAL"
Target "UndoVersion" (fun _ -> UndoVersion module_name)

Description "INTERNAL"
Target "Copy_To_Local_Service" (fun _ -> CopyToLocalService build_directory GetInstallationDirectory)

Description "INTERNAL"
Target "Install_Local_Service" (fun _ -> InstallLocalService GetExecutableName)

Description "INTERNAL"
Target "Start_Local_Service" (fun _ -> StartLocalService service_name)

Description "INTERNAL"
Target "Stop_Local_Service" (fun _ -> StopLocalService service_name )

Description "INTERNAL"
Target "Uninstall" (fun _ -> UninstallService GetExecutableName GetInstallationDirectory)

Description "INTERNAL"
Target "Package" (fun _ -> PackageBuild distribution_directory module_version)

Description "Installs the selectd module on the local machine"
Target "Install" DoNothing

Description "Deploys the selected module"
Target "Deploy" (fun _ -> DeployPackage nugetApiKey nuget_publish_url nuget_endpoint distribution_directory)

Description "Builds the selected module"
Target "Build" DoNothing

Description "Runs tests for the selected module"
Target "Test" (fun _ -> Test build_directory service_name)

Description "Provides a listing of available targets"
Target "Help" (fun _ -> 
    PrintTargets()
)

Description "Shows the status of the Trackwane infrastructure"
Target "Status" (fun _ -> 
    tracefn "Not yet implemented"
)

Description "Shows all available components"
Target "List" (fun _ -> 
    let moduleNames = subDirectories(directoryInfo  "./Modules")
    for moduleName in moduleNames do
        tracefn "%s" moduleName.Name
    tracefn "Framework"
)

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

RunTargetOrDefault "Help"
