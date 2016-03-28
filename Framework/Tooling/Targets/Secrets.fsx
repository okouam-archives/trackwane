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

let secrets = [
    "RabbitMQ.Username"
    "RabbitMQ.Password"
    "Octopus.ApiKey"
    "NuGet.ApiKey"
    "TeamCity.Username"
    "TeamCity.Password"
    "RavenDB.ApiKey"
    "PaperTrail.ApiKey"
    ]

let ShowSecrets = 
    ignore