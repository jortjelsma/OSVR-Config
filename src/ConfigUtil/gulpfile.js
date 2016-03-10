/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf");

var paths = {
	webroot: "./wwwroot/"
};

paths.generated = paths.webroot + "generated";

gulp.task("clean:generated", function (cb) {
	rimraf(paths.generated, cb);
});

gulp.task("clean", ["clean:generated"]);

