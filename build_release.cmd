pushd "%~dp0"
cd src\ConfigUtil
@rem DNU is a batch file, so have to use call or we never return from this
call dnu restore
call dnu publish --runtime active --no-source -o ../../artifacts --configuration Release
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe" ..\Launcher\csproj /build Release
xcopy ..\Launcher\bin\Release\OSVR-Config.exe ..\..\artifacts\ /y
popd
