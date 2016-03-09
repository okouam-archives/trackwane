// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.OctoTools
open Fake.Testing
open Fake.Paket

// Properties
let buildDir = "./.build/"
let appDir = buildDir + "app/"
let testDir  = buildDir + "tests/"

// Targets
Target "Clean" (fun _ ->
  CleanDir buildDir
)

Target "Compile_Application" (fun _ ->
  !! "**/*.csproj"
    |> MSBuildDebug appDir "Build"
    |> Log "Compile-Output: "
)

Target "Compile_Tests" (fun _ ->
  !! "Tests/Tests.csproj"
    |> MSBuildDebug testDir "Build"
    |> Log "TestBuild-Output: "
)

Target "Package_Website" (fun _ ->
  ExecProcess (fun p ->
      p.FileName <- "./packages/OctopusTools/tools/octo.exe"
      p.Arguments <- "pack --id=Trackwane.AccessControl --basePath=" + appDir + "_PublishedWebsites/Web --outFolder=" + buildDir)
      (System.TimeSpan.FromMinutes 2.0)
  |> ignore
)

Target "Run_Tests" (fun _ ->
  !! (testDir + "/*.Tests.dll")
    |> NUnit3 (fun p ->
      {p with
        ToolPath = "./packages/NUnit.Console/tools/nunit3-console.exe"
        OutputDir = testDir
      })
)

Target "Push" (fun _ ->
  Push (fun p ->
    {p with
        ApiKey = "API-KCYS8M508J8YW8UDRX8JZKUNMU"
        PublishUrl = "http://octopus.wylesight.ws/nuget/packages"
        WorkingDir = buildDir
    })
)

// Dependencies
"Clean"
  ==> "Compile_Tests"
//  ==> "Run_Tests"
  ==> "Compile_Application"
  ==> "Package_Website"
  ==> "Push"

// start build
RunTargetOrDefault "Push"
