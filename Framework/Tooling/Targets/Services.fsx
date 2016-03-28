#r "../../packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.FileUtils
open System

let CopyToLocalService build_directory installation_directory =
    cp_r build_directory installation_directory

let InstallLocalService executable =
    Shell.Exec(executable, "install") |> ignore

let StartLocalService service_name =
  startService service_name
  ensureServiceHasStarted service_name (TimeSpan.FromMinutes 1.0) |> ignore

let StopLocalService service_name =
  if checkServiceExists(service_name) then
    StopService service_name
    ensureServiceHasStopped service_name (TimeSpan.FromMinutes 1.0)

let UninstallService executable installation_directory =
    let fileExists = TestFile(executable)
    if fileExists then Shell.Exec(executable, "uninstall") |> ignore
    rm_rf installation_directory


