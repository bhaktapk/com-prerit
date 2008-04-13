@echo off
cls
..\..\Tools\NAnt\NAnt.exe /f:..\Main.build -l:NAnt.log %*
pause
