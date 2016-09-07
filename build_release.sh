#!/bin/sh
(
cd $(dirname $0)
cd src/ConfigUtil
dotnet restore
dotnet publish -o artifacts/bin --configuration Release
)
