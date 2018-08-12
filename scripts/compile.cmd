REM @echo off

IF EXIST .\compile.cmd cd ..

CALL scripts\environnement.cmd

REM --------------------------------------------------------
REM  MSBuild 
REM --------------------------------------------------------
"%MSBUILD%" /t:Build /p:Configuration=Debug;Platform="Any CPU" "Wox-EasyHelper.sln" || exit /b -1
"%MSBUILD%" /t:Build /p:Configuration=Release;Platform="Any CPU" "Wox-EasyHelper.sln" || exit /b -1
