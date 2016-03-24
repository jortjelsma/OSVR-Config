# Dev Tools

## One-time build environment setup scripts

### `win-install-coreclr.cmd`
Does what it says on the tin: follows the instructions to install ASP.NET 5/CoreCLR from the command line on Windows.

### `win-install-nodejs-via-choco.cmd`
If you want to use Chocolatey to install/manage an LTS version of NodeJS, it takes some extra effort (since by default it will install the latest version).
This script and its relatives handle that for you: installing the latest (the last time we checked) LTS NodeJS available through Choco, and pinning it so your `choco upgrade all -y` doesn't accidentally upgrade you out of that branch.

**Must be run as admin** (if your choco install is as admin, which is the standard case)

### `win-upgrade-nodejs-via-choco.cmd`
When we become aware of a newer LTS version, you can use this script to upgrade just to that version safely.

**Must be run as admin** (if your choco install is as admin, which is the standard case)

### `win-config-nodejs-choco-ver.cmd`
**Do not run** this script on its own - it's the common file storing the version number of the "latest NodeJS LTS in Choco", used by `win-upgrade-nodejs-via-choco.cmd` and `win-install-nodejs-via-choco.cmd`.
When a new one is available, only this script needs to be updated, then both the new installs and upgrades will use it right away. (and if you spot one before anyone else, you can pull request this file :D )

### `win-npm-globals.cmd`
Run **after** installing NodeJS (no matter how you install it) - updates NPM itself, then installs the global NPM packages required to build OSVR-Config.
