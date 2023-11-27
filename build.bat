@echo off

del /f allowedCmd.exe

call C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe *.cs

call allowedCmd.exe

pause