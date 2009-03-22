@echo off
cls
..\..\Tools\NAnt\NAnt.exe /f:..\Default.build -l:NAnt.log %*
pause
