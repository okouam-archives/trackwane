@echo off
cls

AccessControl\build.bat "Compile" && Management\build.bat "Compile" && Data\build.bat "Compile" && Simulator\build.bat "Compile"
