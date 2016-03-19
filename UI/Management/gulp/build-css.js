const map = require('lodash/collection/map');

module.exports = (workflow, gulp, $, config) => {

    workflow.subtask('build:css', () =>
        gulp.src(`./src/app.styl`)
            .pipe($.sourcemaps.init())
            .pipe($.stylus({
                compress: config.args.release || config.env.ci,
                'include css': true
            }))
            .pipe($.rename(config.dirs.dist.css))
            .pipe($.sourcemaps.write('./'))
            .pipe($.connect.reload())
            .pipe(gulp.dest('./'))
    );
};
