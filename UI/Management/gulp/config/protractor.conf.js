const path = require('path');
const playgroundConfig = require('./playground.conf');

exports.config = {
    baseUrl: playgroundConfig.url,
    specs: [path.resolve('./e2e/*.spec.js')],
    directConnect: true
};
