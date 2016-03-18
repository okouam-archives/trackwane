const Server = require('karma').Server;

module.exports = (workflow, gulp, $, config) => {
    const karmaConfig = require(config.dirs.karmaConf)(config);

    workflow.subtask('test:unit', (done) => {
        new Server(karmaConfig, done).start();
    });
};
