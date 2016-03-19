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
  $moduleVersion = Get-Module-Version
  ..\.tooling\packages\FAKE\tools\FAKE.exe ../.tooling/tooling.fsx $target module="$moduleName" version="$moduleVersion"
  cd ..
}

function Get-Module-Version() {
  $version = (git describe --long)
  $version -match "(?<longVersion>(?<version>[\d\.]+).+)" | out-null
  $shortVersion = $matches['version']
  $version = $matches['longVersion']
  $x = "git rev-list HEAD --count"
  $commit_count = CMD.EXE /C $x
  $version -match "(?<version1>\d+)(\.(?<version2>\d+)(\.(?<version3>\d+)(\.(?<version4>\d+))?)?)?-(?<version5>[^-]*)-(?<version6>.+)" | out-null
  $version1 = $matches['version1']
  $version2 = if($matches['version2'] -ne $null) { $matches['version2'] } else { "0" }
  $version3 = if($matches['version3'] -ne $null) { $matches['version3'] } else { "0" }
  $version1 + "." + $version2 + "." + $version3 + "." + $commit_count
}
