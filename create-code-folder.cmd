@echo off
RMDIR /S /Q code
MKDIR code
XCOPY app\*.cs code
XCOPY app\*.csproj code