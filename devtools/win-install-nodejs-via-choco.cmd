rem By default Choco will give you the latest NodeJS, not the LTS version,
rem so we keep track of the latest LTS version manually, then pin it to avoid
rem upgrades that aren't intentionally invoked from the script here or naming
rem the involved packages by name.

rem Load the common "Latest LTS version" config
call "%~dp0win-config-nodejs-choco-ver.cmd"

rem Install the specific version
choco install nodejs -version %CHOCO_NODEJS_VER% -y

rem Now we must keep choco from upgrading nodejs on you.
choco pin add -name nodejs
choco pin add -name nodejs.install
