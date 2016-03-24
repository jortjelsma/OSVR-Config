rem Make sure you have NodeJS installed first. If you're using Chocolatey,
rem might I suggest win-install-nodejs-via-choco.cmd first...

rem Update NPM itself first.
npm update -g npm

rem Install bower for front-end dependency fetching
npm install -g bower

rem Install gulp for prepublish script for front end build tasks
npm install -g gulp

pause
