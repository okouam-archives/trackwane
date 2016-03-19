param([String] $module)

. .tooling\tooling.ps1
Run-Target Compile $module
