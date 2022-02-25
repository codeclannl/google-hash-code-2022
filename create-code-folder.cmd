@echo off
RMDIR /S /Q code
MKDIR output\code
XCOPY app\*.cs output\code
XCOPY app\*.csproj output\code