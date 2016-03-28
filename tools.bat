@echo off
cls
REM "NuGet.exe" "Install" "FAKE" "-OutputDirectory" "Framework\packages" "-ExcludeVersion"
"Framework\packages\FAKE\tools\Fake.exe" Framework\Tooling\tools.fsx %*
