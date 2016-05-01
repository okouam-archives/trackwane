#r "../../packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.OctoTools
open Fake.Testing
open System
open Fake.Paket
open Fake.FileUtils
open Fake.PaketTemplate
open Fake.AssemblyInfoFile
open Fake.Git

let DeployPackage nugetApiKey nuget_publish_url nuget_endpoint distribution_directory =
  Fake.Paket.Push (fun p ->
    {p with ApiKey = nugetApiKey; PublishUrl = nuget_publish_url; EndPoint = nuget_endpoint; WorkingDir = distribution_directory})

let PackageBuild distribution_directory module_version =
  MSBuildDebug null "Build" ["Standalone/Standalone.csproj"]
    |> ignore
  rm_rf distribution_directory
  Shell.Exec(".paket\paket.exe", "pack output " + distribution_directory + " templatefile paket.template version " + module_version) |> ignore