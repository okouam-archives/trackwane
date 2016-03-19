const del = require('del');

module.exports = (workflow, gulp, $, config) => {
    workflow.subtask('clean', () => del.sync(config.dirs.dist.root));
};
