module.exports = (workflow, gulp, $, config) => {
    workflow.subtask('eslint', () =>
        gulp.src('**/*.js')
            .pipe($.eslint())
            .pipe($.eslint.format())
            .pipe($.if(config.args.release || config.env.ci, $.eslint.failAfterError()))
    );
};
