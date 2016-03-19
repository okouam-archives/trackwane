function Bootstrap() {
  .paket\paket.bootstrapper.exe
  .paket\paket.exe restore
  nuget install FAKE -ExcludeVersion -OutputDirectory ../.tooling/packages
}

function Check-Module-Name($module) {
  Clear-Host
  if (-Not (Test-Path $module)) {
    Write-Host "ERROR: The Trackwane module <$module> cannot be located. Please check the module name." -foregroundcolor "red"
    exit
  }
}

function Clean-Module-Name($module) {
  $module
}

function Run-Target($target, $module) {
  $moduleName = Clean-Module-Name $module
  Check-Module-Name $moduleName
  cd $moduleName
  Bootstrap
  ..\.tooling\packages\FAKE\tools\FAKE.exe ../.tooling/tooling.fsx $target module="$moduleName"
  cd ..
}
