@echo off
cls

set startpath=%cd%

cd %~dp0\

.paket\paket.bootstrapper.exe
if errorlevel 1 (
    exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
    exit /b %errorlevel%
)

packages\FAKE\tools\FAKE.exe build.fsx Deploy

cd %startpath%
