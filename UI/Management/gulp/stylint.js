module.exports = (workflow, gulp, $, config) => {
    const reporter = (config.args.release || config.env.ci) ? 'fail' : undefined;

    workflow.subtask('stylint', () =>
        gulp.src(config.dirs.src.styl)
            .pipe($.stylint())
            .pipe($.stylint.reporter(reporter))
    );
};
