#!/bin/sh
(
cd $(dirname $0)
cd src/ConfigUtil
dotnet restore
dotnet publish --runtime active --no-source -o ../../artifacts --configuration Release
)
