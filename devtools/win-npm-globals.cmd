rem Make sure you have NodeJS installed first. You might need to do something like
rem choco install nodejs -version 4.4.0 -y
rem choco pin add -name nodejs
rem first...

rem Update NPM itself first.
npm update -g npm

rem Install bower for front-end dependency fetching
npm install -g bower

rem Install gulp for prepublish script for front end build tasks
npm install -g gulp

pause
