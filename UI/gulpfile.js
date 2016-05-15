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
	eslint = require("gulp-eslint"),
	plumber = require("gulp-plumber"),
	notify  = require('gulp-notify'),
 	source = require('vinyl-source-stream');
	var stylish = require('jshint-stylish');
	var jshint = require('gulp-jshint');

var css_sources = ["src/**/*.styl", "src/app.styl"];
var js_sources = ["src/**/*.js", "!src/bundles/**/*.*", "!src/bower_components/**/*.*"];
var dist = './src/bundles';

gulp.task("css", function () {
    return gulp
			.src(css_sources)
			.pipe(plumber({
				errorHandler: notify.onError('Error: <%= error.message %>')
			}))
            .pipe(stylus())
			.pipe(cssmin())
			.pipe(concat('trackwane.min.css'))
            .pipe(gulp.dest(dist));
});

gulp.task("js", function () {
  gulp
  	.src(js_sources)
  	.pipe(plumber({
		errorHandler: notify.onError('Error: <%= error.message %>')
	}))
  	.pipe(angularjsx())
	.pipe(babel())
	.pipe(eslint())
	.pipe(eslint.format())
    .pipe(concat("trackwane.min.js"))
    .pipe(gulp.dest("src/bundles"));
});

gulp.task('watch', function () {
    gulp.watch(js_sources.concat(css_sources), ['css', 'js']);
});

gulp.task('default', ['css', 'js', 'watch']);
