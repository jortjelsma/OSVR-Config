setlocal
set DOTNET_CLI_TELEMETRY_OPTOUT=1
pushd "%~dp0"

call dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%

cd src\ConfigUtil

call dotnet publish -r win7-x64 -o artifacts\bin --configuration Release
if %errorlevel% neq 0 exit /b %errorlevel%

xcopy artifacts\bin ..\..\artifacts\bin\ /Y /E
if %errorlevel% neq 0 exit /b %errorlevel%

call "%VS140COMNTOOLS%vsvars32.bat"

msbuild ..\Launcher\Launcher.csproj /p:Configuration=Release
if %errorlevel% neq 0 exit /b %errorlevel%

xcopy ..\Launcher\bin\Release\OSVR-Config.exe ..\..\artifacts\ /y
if %errorlevel% neq 0 exit /b %errorlevel%
popd
endlocal
