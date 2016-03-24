# OSVR-Config
OSVR-Config is a utility to configure the OSVR Server, and gives you access to a few OSVR-related tools.

# Build instructions
## Prerequisites
 * Install Asp.Net 5.0 (Asp.Net Core 1.0) tools
   * Asp.Net 5.0 is in the process of being renamed Asp.Net Core 1.0, and in addition the .Net command line tools are being reworked and simplified. For now, however, we are still using the Asp.Net 5.0 version with the DNX command line tools. This is a temporary situation as everything migrates over.
   * Click [here](https://docs.asp.net/en/latest/getting-started/index.html) for instructions on installing ASP.Net for your platform. We are using CoreCLR exclusively, so when the instructions give you the option to use CoreCLR or the full .Net CLR (on windows), choose CoreCLR by running `dnvm upgrade -r coreclr`.
   * If you already have ASP.Net installed (via Visual Studio/etc..), we are specifically using version `1.0.0-rc1-update1` (`x86` or `x64` is your choice) and the `coreclr` runtime. So, for example, you can set the default runtime by running `dnvm use 1.0.0-rc1-update1 -r coreclr -p` and then `dnvm upgrade`. This will add the command line tools to your `PATH`.
 * Install NodeJS from [here](https://nodejs.org/)
   * We're currently running with the `4.4.1 LTS` version. The `5.5.1 Stable` version may also work, but this is untested.
   * The version of `npm` installed with NodeJS is always out of date. Run `npm update -g npm` to get the latest version. Otherwise some `npm` dependencies may squawk at you that your `npm` version is out of date.
 * Various global npm tools needed for the frontend build
   * `npm install -g bower` - Used by the prepublish script to get the latest frontend dependencies.
   * `npm install -g gulp` - Used by the prepublish script for frontend build tasks.
   * `npm install -g tsd` - (Optional) You may use this to install new TypeScript typings or update existing ones (not often used).
   * `npm install -g rimraf` (Optional) I only recommend this on Windows, and only with great caution. rimraf will delete directories recursively. It's only useful on Windows because it bypasses all the long file path issues introduced by `npm`'s `node_modules` folder. Please be careful - it does not prompt you for confirmation.

## IDE support
 * Visual Studio 2015, with the latest updates, and with the latest RC1-update1 version of the ASP.Net and web tool extensions.
 * Visual Studio Code (with C# extension installed). You may need to build from the command line.
 * Atom.io (with C# and TypeScript extensions installed). You may need to build from the command line.

## Build
### Command Line
 * Set your working directory to `/OSVR-Config/src/ConfigUtil`
 * Run the following to download backend NuGet packages: `dnu restore`.
 * Run the following: `dnu publish --runtime active --no-source -o ../../artifacts`
   * This will install frontend dependencies and build the frontend prior to publishing.

### Visual Studio
* Visual Studio's CoreCLR/Asp.Net 5.0 and Web tools extensions are all integrated into the IDE.
  * In the Run button dropdown, ensure that `web` is selected. This makes the backend run as a standalone server. Avoid using IIS Express.
  * Also in the Run button dropdown, ensure that the .Net Runtime Type is set to Core CLR.
  * The build, debug, and clean commands in Visual Studio will build both the frontend and the backend.
  * There is a publish profile called `filesystem` that builds the frontend and backend into `/artifacts/bin/ConfigUtil/PublishOutput`. Run the `Publish ConfigUtil` menu item from the `Build` menu. Keep in mind this is not the `Release` build that is published - currently you must use the command line to build the standalone release binary (see instructions above).

# Running OSVR-Config
 * Once you've published the application with `dnu`, you should have a `web.cmd` (for Windows) and/or a `web` shell script (for Mac/Linux) located in `/OSVR-Config/artifacts/approot`. Run this to start the backend.
 * Once the backend is running, go to [http://localhost:5000](http://localhost:5000) in a browser to start the application.
 * There is a github issue to make this a one-click process. See https://github.com/OSVR/OSVR-Config/issues/9.
