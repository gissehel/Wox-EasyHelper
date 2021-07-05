@echo off
GITHUB_REF=%1
GITHUB_ENV=%2
echo|set /p="REFNAME=" >> "%GITHUB_ENV%"
echo %GITHUB_REF% | scripts\sed.exe -e "s/tags\/v/tags\//; s/.*\///" >> "%GITHUB_ENV%"
