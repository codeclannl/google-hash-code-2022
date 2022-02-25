@echo off
RMDIR /S /Q output\code
MKDIR output\code
XCOPY app\*.cs output\code
XCOPY app\*.csproj output\code