#!/bin/sh
(
export DOTNET_CLI_TELEMETRY_OPTOUT=1
echo Setting DOTNET_CLI_TELEMETRY_OPTOUT=${DOTNET_CLI_TELEMETRY_OPTOUT}
cd $(dirname $0)
cd src/ConfigUtil
dotnet restore
dotnet publish -o artifacts/bin --configuration Release
cp scripts/osvr-config.sh artifacts
)
