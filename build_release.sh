#!/bin/sh
(
cd $(dirname $0)
cd src/ConfigUtil
dnu restore
dnu publish --runtime active --no-source -o ../../artifacts --configuration Release
)
