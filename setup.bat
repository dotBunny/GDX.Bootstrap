:: Clear screen
cls

:: Clone Package Repo
git clone https://github.com/dotBunny/GDX.git %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx
pushd %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx\
git checkout dev
popd

git clone https://github.com/dotBunny/GDX.git %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx
pushd %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx\
git checkout dev
popd

:: Make Project Links
mklink %~dp0Projects\GDX_Development\.editorconfig %~dp0Projects\GDX_Development\Packages\com.dotbunny.gdx\.editorconfig
mklink %~dp0Projects\GDX_Entities\.editorconfig %~dp0Projects\GDX_Entities\Packages\com.dotbunny.gdx\.editorconfig

:: Clone Dox Repo
git clone https://github.com/dotBunny/GDX.Dox.git %~dp0Projects\Dox

pause