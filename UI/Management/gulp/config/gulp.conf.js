const NAME = 'trackwane-ui-management';
const SRC = './src';
const APP = `${SRC}/!(*e2e)/**/!(*tests).js`;
const MAIN = `${SRC}/app.js`;
const TEMPLATES = `${SRC}/**/*.tpl.html`;
const STYL = `${SRC}/**/*.styl`;
const DIST = './dist/';
const DISTCSS = `${DIST}app.css`;
const KARMACONF = './config/karma.conf';

module.exports = {
    name: NAME,
    env: {
        dev: process.env.TEAMCITY_VERSION === undefined,
        ci: process.env.TEAMCITY_VERSION !== undefined
    },
    dirs: {
        src: {
            root: SRC,
            app: APP,
            main: MAIN,
            templates: TEMPLATES,
            styl: STYL
        },
        dist: {
            root: DIST,
            css: DISTCSS,
        },
        karmaConf: KARMACONF
    }
};
