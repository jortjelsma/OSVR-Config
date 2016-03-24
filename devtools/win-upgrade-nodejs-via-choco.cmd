rem By default Choco will give you the latest NodeJS, not the LTS version
rem so we can't just freely let it upgrade. This will let it upgrade just
rem to the latest version that is mentioned in this repo, and assumes you
rem have nodejs and nodejs.install pinned

rem Load the common "Latest LTS version" config
call "%~dp0win-config-nodejs-choco-ver.cmd"

choco upgrade nodejs -version %CHOCO_NODEJS_VER% -y
