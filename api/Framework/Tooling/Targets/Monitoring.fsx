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

let CheckTeamCity = 
    ignore

let CheckConsul = 
    ignore

let CheckRavenDB = 
    ignore

let CheckRabbitMQ = 
    ignore

let CheckPapertrail = 
    ignore

let CheckOctopus = 
    ignore

let CheckPackageFeed = 
    ignore

let CheckTrackwane = 
    ignore

let Status = 
    tracefn "Checking Production Environment"
    CheckTeamCity()
    CheckOctopus()
    CheckPackageFeed()
    CheckRabbitMQ()
    CheckRavenDB()
    CheckConsul()
    CheckPapertrail()
    CheckTrackwane()
    tracefn "Checking Development Environment"
    CheckRabbitMQ()
    CheckRavenDB()
    CheckConsul()
    CheckPapertrail()
    CheckTrackwane()
