Clear-Host

install-module Octopus-Cmdlets
Import-Module Octopus-Cmdlets

# Get list of modules
$modules = dir -Directory

Write-Host "================ INSTALLED TRACKWANE MODULES =======================" -foregroundcolor Blue
Foreach($module in $modules) {
  $service = "$module/.local/Trackwane.$module.Standalone.exe"
  if (Test-Path $service) {
    $version = (Get-Command $service).FileVersionInfo.FileVersion
    Write-Host "$module ($version)"
  }
}

Write-Host "================ TRACKWANE MODULE RELEASES =======================" -foregroundcolor Blue
Connect-OctoServer http://octopus.wylesight.ws API-MCZZVTPXAJ6XV2VSHYUMPCLOGN8
#Foreach($module in $modules) {
#  if (Test-Path "$module/paket.template") {
#    $releases = Get-OctoRelease $module
#    Foreach($release in $releases) {
#        $deployment = Get-OctoDeployment -ReleaseId $release.Id
#        $deployment | Get-Member | sort Name
#        $release | Get-Member | sort Name
#        $taskId = $deployment.TaskId
#        Write-Host $release.Project $release.version $deployment.Created
#    }
#  }
#}
# Get installed modules with their status

Write-Host "================ TRACKWANE NUGET PACKAGES =======================" -foregroundcolor Blue

# Get deployed version for each module
