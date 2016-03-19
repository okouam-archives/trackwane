const gulp = require('gulp');
const workflow = require('gulp-workflow');

workflow
    .load(gulp)
    .task('build', 'Build the application.', ['clean', ['build:js', 'build:css']], {
        release: 'Optimise the compiled code for a production release.'
    })
    .task('ci', 'Lint, test and build the application.', ['lint', 'build', 'test'])
    .task('dev', 'Build the application, watch for changes and re-compile.', ['build', 'serve', 'watch'])
    .task('lint', 'Run all linters.', ['eslint', 'stylint'])
    .task('serve', 'Create a server running the application.', ['connect'])
    .task('test', 'Run unit and e2e tests.', ['test:unit', 'serve', 'test:e2e', 'connect:kill'], {
        watch: 'Re-run unit tests when a source or test file is changed.'
    });
