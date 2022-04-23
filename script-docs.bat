@echo off

:: Clear screen
cls

:: Clone Dox Repo
echo Dox project ...
if exist %~dp0Projects\Dox\ (
  echo Found.
) else (
  echo Cloning ...
  git clone https://github.com/dotBunny/GDX.Dox.git %~dp0Projects\Dox
)

:: Build Dox
echo Building Dox
dotnet build Projects/Dox/Dox.sln

dotnet Projects/Dox/bin/Debug/Dox.dll

pause