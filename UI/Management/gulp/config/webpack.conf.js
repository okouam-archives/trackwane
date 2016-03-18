const path = require('path');
const webpack = require('webpack');

module.exports = (config) => {
    config = config || {};

    const plugins = [
        new webpack.DefinePlugin({
            __DEV__: config.env.dev,
            __CI__: config.env.ci,
            __PROD__: config.args.release
        })
    ];

    if (!config.TEST && (config.args.release || config.env.ci)) {
        plugins.push.apply(plugins, [
            new webpack.optimize.DedupePlugin(),
            new webpack.optimize.OccurrenceOrderPlugin(),
            new webpack.optimize.UglifyJsPlugin()
        ]);
    }

    return {
        entry: config.dirs.src.main,
        output: {
            path: path.resolve(config.dirs.dist.root),
            filename: `app.js`
        },
        plugins,
        module: {
            loaders: [{
                test: /\.js/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel',
                query: {
                    plugins: ['transform-runtime']
                }
            }, {
                test: /\.html$/,
                loader: 'html'
            }]
        },
        devtool: (config.env.dev && !config.args.release) ? 'eval' : 'source-map'
    };
};
