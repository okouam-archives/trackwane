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
  !! "Tests/Framework.Tests.csproj"
    |> MSBuildDebug testDir "Build"
    |> Log "TestBuild-Output: "
)

Target "Run_Tests" (fun _ ->
  !! (testDir + "/*.Tests.dll")
    |> NUnit3 (fun p ->
      {p with
        ToolPath = "./packages/NUnit.Console/tools/nunit3-console.exe"
        OutputDir = testDir
      })
)

Target "Compile" DoNothing

"Clean"
  ==> "Compile_Tests"
  ==> "Compile_Application"
  ==> "Compile"

"Compile"
  ==> "Run_Tests"

RunTargetOrDefault "Compile"
