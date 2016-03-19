const webpack = require('webpack');

module.exports = (workflow, gulp, $, config) => {
    workflow.subtask('build:js', (done) => {
        const webpackConfig = require('./config/webpack.conf')(config);
        const devWebpackCompiler = webpack(webpackConfig);

        if (config.args.watch) {
            devWebpackCompiler.watch({}, handler());
        } else {
            devWebpackCompiler.run(handler(() => {
                gulp.src('').pipe($.connect.reload());
                done();
            }));
        }
    });

    function handler(done) {
        return (err, stats) => {
            if (err) {
                throw new $.util.PluginError('webpack', err);
            }

            $.util.log(stats.toString({
                chunks: false,
                colors: true
            }));

            if (done) {
                done();
            }
        };
    }
};
