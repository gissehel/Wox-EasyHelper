:: try to locate the latest installation of VS and from there, MSBuild.exe
:: if VSWHERE is missing then we use the hardcoded path from before VS2017
SET VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe

if exist "%VSWHERE%" (
  for /f "usebackq tokens=1* delims=: " %%i in (`"%VSWHERE%" -latest -products * -requires Microsoft.Component.MSBuild`) do (
    if /i "%%i"=="installationPath" set MSBUILD=%%j\MSBuild\15.0\Bin\MSBuild.exe
  )
) ELSE (
  set "MSBUILD=%ProgramFiles(x86)%\MSBuild\14.0\Bin\MsBuild.exe"
)

SET TARGET_DIR=target
SET ARTEFACT_DIR=artefacts

IF EXIST scripts\local-environnement.cmd CALL scripts\local-environnement.cmd
