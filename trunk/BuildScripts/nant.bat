@echo off
cls
..\Tools\nant\NAnt.exe -buildfile:Main.build %*
pause