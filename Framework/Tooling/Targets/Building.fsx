#r "../../packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing
open System
open Fake.Paket
open Fake.Git
open Fake.FileUtils
open Fake.PaketTemplate
open Fake.AssemblyInfoFile

let GetCodebaseVersion = 
    let git_version = runSimpleGitCommand "." "describe --long"
    let git_commit_count = runSimpleGitCommand "." "rev-list HEAD --count"
    git_version.Split('-').[0] + "." + git_commit_count

let GetComponentRoot (component_name) = 
    if directoryExists ("./Modules/" + component_name)
    then "./Modules/" + component_name
    else "./" + component_name  

let Clean build_directory = 
  CleanDir build_directory

let Compile component_name build_directory = 
    if (isNullOrEmpty(component_name)) then failwithf "No Trackwane component was provided"
    !! (GetComponentRoot(component_name) + "/**/*.csproj")
        |> MSBuildDebug build_directory "Build"
        |> ignore

let UndoVersion module_name =
    if (isNullOrEmpty(module_name)) then failwithf "No Trackwane component was provided"
    let files = !! (GetComponentRoot(module_name) + "/**/AssemblyInfo.cs")
    for file in files do
        let description = GetAttributeValue "AssemblyDescription" file
        let new_attribute = Attribute.Description(description.Value.Replace("\"", ""))
        CreateCSharpAssemblyInfo file [new_attribute]

let Version service_name module_name =
    if (isNullOrEmpty(module_name)) then failwithf "No Trackwane module name was provided"
    let git_version = GetCodebaseVersion
    traceImportant("Trackwane: Using assembly version " + git_version) |> ignore
    let file = "./Modules/" + module_name + "/Standalone/Properties/AssemblyInfo.cs"
    let description = GetAttributeValue "AssemblyDescription" file
    CreateCSharpAssemblyInfo file
        [Attribute.Title service_name;
        Attribute.Description(description.Value.Replace("\"", ""));
        Attribute.Product service_name;
        Attribute.Version git_version;
        Attribute.FileVersion git_version]

let Test build_directory service_name =
    if (isNullOrEmpty(service_name)) then failwithf "No Trackwane module name was provided"
    !! (build_directory + "/" + service_name + ".Tests.dll")
        |> NUnit3 (fun p ->
            {p with
                ToolPath = "./packages/NUnit.Console/tools/nunit3-console.exe"
        })




