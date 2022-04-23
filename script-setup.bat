@echo off

:: Clear screen
cls

:: Clone Development Repository
echo GDX package in GDX_Devlepment project ...
if exist %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx\ (
  echo Found.
) else (
  echo Cloning ...
  git clone https://github.com/dotBunny/GDX.git %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx
  pushd %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx\
  git checkout dev
  popd
  echo Selected Dev Branch.
)

:: Clone Entities Repository
echo GDX package in GDX_Entities project ...
if exist %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx\ (
  echo Found.
) else (
  echo Cloning ...
  git clone https://github.com/dotBunny/GDX.git %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx
  pushd %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx\
  git checkout dev
  popd
  echo Selected Dev Branch.
)

:: Make Development EditorConfig Symlink
echo GDX_Development .editorconfig Symlink
if exist %~dp0Projects\GDX_Development\.editorconfig (
  echo Found.
) else (
  mklink %~dp0Projects\GDX_Development\.editorconfig %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx\.editorconfig
  echo Created.
)

:: Make Entities EditorConfig Symlink
echo GDX_Development .editorconfig Symlink
if exist %~dp0Projects\GDX_Entities\.editorconfig (
  echo Found.
) else (
  mklink %~dp0Projects\GDX_Entities\.editorconfig %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx\.editorconfig
  echo Created.
)

pause