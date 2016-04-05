pushd "%~dp0"
cd src\ConfigUtil
@rem DNU is a batch file, so have to use call or we never return from this
call dnu restore
call dnu publish --runtime active --no-source -o ../../artifacts --configuration Release
call "%VS140COMNTOOLS%vsvars32.bat"
call devenv ..\Launcher\Launcher.csproj /build Release
msbuild ..\Launcher\Launcher.csproj /p:Configuration=Release
xcopy ..\Launcher\bin\Release\OSVR-Config.exe ..\..\artifacts\ /y
popd
