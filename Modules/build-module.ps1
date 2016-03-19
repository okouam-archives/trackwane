param(
  [String] $module
)

Clear-Host

nuget install FAKE -ExcludeVersion

cd $module

.paket\paket.bootstrapper.exe

.paket\paket.exe restore

packages\FAKE\tools\FAKE.exe ../.tooling/tooling.fsx Compile module="$module"

cd ..