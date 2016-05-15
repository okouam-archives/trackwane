/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
 	sourcemaps = require("gulp-sourcemaps"),
	stylus = require('gulp-stylus'),
 	babel = require("gulp-babel"),
	browserify = require('browserify'),
	angularjsx = require("gulp-angular-jsx"),
	path = require("path"),
 	source = require('vinyl-source-stream');

var css = ["./src/**/*.styl", "./src/app.styl"];
var dist = './src/bundles';

gulp.task("css", function () {
    return gulp.src(css)
            .pipe(stylus())
			.pipe(cssmin())
			.pipe(concat('trackwane.min.css'))
            .pipe(gulp.dest(dist));
});

gulp.task('watch', function () {
    gulp.watch(["./src/**/*.styl", "./src/app.styl", "src/**/*.js", "!./src/bundles/*.*", "!./src/bower_components/*.*"], ['css', 'default']);
});

gulp.task("default", function () {
  gulp.src(["src/**/*.js", "!./src/bundles/*.*", "!./src/bower_components/*.*"])
  	.pipe(angularjsx())
    .pipe(concat("trackwane.min.js"))
    .pipe(gulp.dest("src/bundles"));
});
