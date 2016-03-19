module.exports = (workflow, gulp, $, config) => {
    workflow.subtask('watch', () => {
        gulp.watch([config.dirs.src.app, config.dirs.src.templates], ['build:js']);
        gulp.watch([config.dirs.src.tokens, config.dirs.src.styl], ['build:css']);
    });
};
