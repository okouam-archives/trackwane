const path = require('path');
const playgroundConfig = require('./config/playground.conf');

const connectConfig = {
    root: path.resolve('./'),
    livereload: true,
    port: playgroundConfig.port
};

module.exports = (workflow, gulp, $) => {
    workflow.subtask('connect', () => {
        $.connect.server(connectConfig);
    });

    workflow.subtask('connect:kill', () => {
        $.connect.serverClose();
    });
};
