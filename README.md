# OSVR-Config
OSVR-Config is a utility to configure the OSVR Server, and gives you access to a few OSVR-related tools.

# Pre-built Binaries
Download the latest OSVR-Config build [here](http://access.osvr.com/binary/osvr_config).

# Build instructions
## Prerequisites
 * OS Notes:
   * Ubuntu users may want to stick to the long-term support release (14.04). You may see errors running the latest 15.* or 16.* versions.
 * Install CoreCLR tools
   * We are now using the CoreCLR RTM version, including the new dotnet command line tools.
   * Click [here](https://www.microsoft.com/net/core) for instructions on installing CoreCLR for your platform.
     * (Linux) Don't forget to run the script provided to uninstall previous versions of dotnet command line tools (dnvm/dnx).
   * If you are using Visual Studio, you will need Visual Studio 2015 Update 3 with [.NET Core 1.0.0 - VS 2015 Tooling Preview 2](https://go.microsoft.com/fwlink/?LinkId=817245) installed.
     * See [this](https://docs.microsoft.com/en-us/dotnet/articles/core/windows-prerequisites#issues) note about installation issues with this version.
     * Note: you don't need VS 2015 or the extension if you're going to be building from the command line and using another editor like VS Code or Atom.
 * Install NodeJS from [here](https://nodejs.org/)
   * We're currently running with the `4.4.1 LTS` version. The `5.5.1 Stable` version may also work, but this is untested.
   * NOTE: Depending on where you have NodeJS installed in Linux, you may need to prepend `sudo` to some of the npm commands with '-g'. If you get a permission denied error, try running with `sudo`.
   * The version of `npm` installed with NodeJS is always out of date. Run `npm update -g npm` to get the latest version. Otherwise some `npm` dependencies may squawk at you that your `npm` version is out of date.
 * Various global npm tools needed for the frontend build
   * `npm install -g bower` - Used by the prepublish script to get the latest frontend dependencies.
   * `npm install -g gulp` - Used by the prepublish script for frontend build tasks.
   * `npm install -g tsd` - (Optional) You may use this to install new TypeScript typings or update existing ones (not often used).
   * `npm install -g rimraf` (Optional) I only recommend this on Windows, and only with great caution. rimraf will delete directories recursively. It's only useful on Windows because it bypasses all the long file path issues introduced by `npm`'s `node_modules` folder. Please be careful - it does not prompt you for confirmation.
   * Windows users: There's a script in `devtools` that will handle updating `npm` and installing just the required global tools for you.

## IDE support
If you'd like to help with development, and not just build the project:

 * Visual Studio 2015 Update 3, and with [.NET Core 1.0.0 - VS 2015 Tooling Preview 2](https://go.microsoft.com/fwlink/?LinkId=817245) installed.
 * Visual Studio Code (with C# extension installed). You may need to build from the command line.
 * Atom.io (with C# and TypeScript extensions installed). You may need to build from the command line.

## Build
### Command Line - Scripted
 * Run the `build_release.cmd` (Windows) or `build_release.sh` (Linux/OS X) script.
   * This will do the same thing as the following section: download backend NuGet packages, install frontend dependencies, build the frontend, then publish.

### Command Line - Manually
 * Set your working directory to the `src/ConfigUtil` directory in this project.
 * Run the following to download backend NuGet packages: `dotnet restore`.
 * Run the following: `dotnet publish -o artifacts --configuration Release`
   * This will install frontend dependencies and build the frontend prior to publishing.

### Visual Studio
* Visual Studio's CoreCLR/ASP.NET and Web tools extensions are all integrated into the IDE.
  * In the Run button dropdown, ensure that `OSVRConfig` is selected. This makes the backend run as a standalone server. Avoid using IIS Express.
  * The build, debug, and clean commands in Visual Studio will build both the frontend and the backend.

# Running OSVR-Config
 * Once you've published the application with `dotnet`, you should have a `OSVRConfig.exe` (for Windows) and/or a `OSVRConfig` binary (for Mac/Linux) located in the `artifacts/bin` directory within this repository's root directory. Run this to start the backend.
 * Once the backend is running, go to [http://localhost:5000](http://localhost:5000) in a browser to start the application.
