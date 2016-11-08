setlocal
set DOTNET_CLI_TELEMETRY_OPTOUT=1
pushd "%~dp0"
cd src\ConfigUtil
@rem DNU is a batch file, so have to use call or we never return from this
call dotnet restore
call dotnet publish -r win7-x64 -o artifacts\bin --configuration Release
xcopy artifacts\bin ..\..\artifacts\bin\ /Y /E
call "%VS140COMNTOOLS%vsvars32.bat"
msbuild ..\Launcher\Launcher.csproj /p:Configuration=Release
xcopy ..\Launcher\bin\Release\OSVR-Config.exe ..\..\artifacts\ /y
popd
endlocal