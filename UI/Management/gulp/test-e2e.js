module.exports = (workflow, gulp, $) => {
    workflow.subtask('webdriver:update', $.protractor.webdriver_update);

    workflow.subtask('test:e2e', ['webdriver:update'], () =>
        gulp.src([])
            .pipe($.protractor.protractor({
                configFile: './gulp/config/protractor.conf.js'
            }))
            .on('error', (e) => {
                throw e;
            }));
};
