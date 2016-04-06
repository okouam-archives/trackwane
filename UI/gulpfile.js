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
    sass = require("gulp-sass"),
	webpack = require("webpack"),
	path = require("path"),
    gulpWebpack = require('gulp-webpack');

var paths = {
    webroot: "./wwwroot/"
};

paths.module_js = "./src/modules/**/*.js";
paths.module_scss = "./src/modules/**/*.styl";
paths.application_js = "./src/application/**/*.js";
paths.application_scss = "./src/application/**/*.styl";
paths.dist = './dist';

var webpackConfig = {
	devtool: 'source-map',
    debug: true,
    watch: true,
    entry: {
        app: "./src/application/application.js",
        vendor: ['angular', 'angular-google-maps', 'angular-ui-router', 'ng-reflux']
    },
    resolve: {
        modulesDirectories: ["node_modules"]
    },
    output: {
		path: path.resolve(__dirname, paths.dist),
        filename: "trackwane.min.js"
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: 'babel',
                query: {
                    presets: ['es2015']
                }
            }
        ]
    },
	plugins: [
		new webpack.optimize.CommonsChunkPlugin('vendor', 'vendor.bundle.js')
	]
};

gulp.task("webpack", function () {
    return gulp.src(paths.webroot + "./src/application/application.js")
        .pipe(gulpWebpack(webpackConfig))
        .pipe(gulp.dest(paths.dist));
});

gulp.task("clean", function (cb) {
    rimraf(paths.dist, cb);
});

gulp.task("css", function () {
    return gulp.src([paths.module_scss, paths.application_scss])
            .pipe(stylus())
			.pipe(cssmin())
			.pipe(concat('trackwane.min.css'))
            .pipe(gulp.dest(paths.dist));
});

gulp.task('watch', function () {
    gulp.watch([
			paths.module_js,
			paths.module_scss,
			paths.application_js,
			paths.application_scss
	 	],
		['js', 'css']
	);
});
