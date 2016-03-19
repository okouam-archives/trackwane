function Bootstrap() {
  .paket\paket.bootstrapper.exe
  .paket\paket.exe restore
  nuget install FAKE -ExcludeVersion -OutputDirectory ../.tooling/packages
}

function Check-Module-Name($module) {
  $moduleName = $module
  if ([string]::IsNullOrEmpty($module)) {
    $availableModules = Get-ChildItem . | ?{ $_.PSIsContainer } | Where {!$_.Name.StartsWith(".")}
    Write-Host "Available modules"  -foregroundcolor "Green"
    Foreach($availableModule in $availableModules) {
      Write-Host ">" $availableModule.Name
    }
    $moduleName = Read-Host '>'
  }
  if (-Not (Test-Path $moduleName)) {
    Write-Host "ERROR: The Trackwane module <$moduleName> cannot be located. Please check the module name." -foregroundcolor "red"
    exit
  }
  $moduleName
}

function Clean-Module-Name($module) {
  $module
}

function Run-Target($target, $module) {
  Clear-Host
  Write-Host "=====================================================================" -foregroundcolor Blue
  Write-Host "Running FAKE target :: $target" -foregroundcolor Blue
  Write-Host "=====================================================================" -foregroundcolor Blue
  $moduleName = Clean-Module-Name $module
  $selectedModule = Check-Module-Name $moduleName
  cd $selectedModule
  Bootstrap
  $moduleVersion = Get-Module-Version
  Write-Host "=====================================================================" -foregroundcolor Blue
  Write-Host "Bootstrapping complete for $selectedModule ($moduleVersion)" -foregroundcolor Blue
  Write-Host "=====================================================================" -foregroundcolor Blue
  ..\.tooling\packages\FAKE\tools\FAKE.exe ../.tooling/tooling.fsx $target module="$selectedModule" version="$moduleVersion"
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
