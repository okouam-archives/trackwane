const merge = require('lodash/object/merge');
const testsConfig = require('path').resolve('./gulp/config/testsEntry.conf.js');

module.exports = (config) => {
    const webpackConfig = require('./webpack.conf')(Object.assign({}, config, { TEST: true }));
    const reporters = [(config.env.ci) ? 'teamcity' : 'dots'];

    return {
        basePath: '../',

        frameworks: ['jasmine'],

        files: [testsConfig],

        exclude: ['karma/'],

        preprocessors: {
            [testsConfig]: ['webpack', 'sourcemap']
        },

        reporters: (config.args.watch) ? reporters : reporters.concat('coverage'),

        coverageReporter: {
            dir: config.dirs.coverage,
            type: 'html'
        },

        webpack: merge(webpackConfig, {
            entry: {},
            output: {},
            module: {
                preLoaders: [{
                    test: /\.js$/,
                    exclude: [/(node_modules|bower_components|config)/, /\.test\.js$/],
                }],
            },
            devtool: 'inline-source-maps'
        }),

        webpackMiddleware: {
            noInfo: true
        },

        port: 9876,

        colors: true,

        logLevel: 'ERROR',

        autoWatch: config.args.watch,

        browsers: ['PhantomJS'],

        singleRun: !config.args.watch
    };
};
