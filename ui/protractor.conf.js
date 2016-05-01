require("babel-core/register")({
    presets: [
        "es2015"
    ]
});

exports.config = {
  	seleniumAddress: 'http://localhost:4444/wd/hub',
  	specs: ['./tests/**/*.spec.js'],
  	framework: 'jasmine',
  	jasmineNodeOpts: {
  		onComplete: function () {},
  		isVerbose: true,
  		showColors: true,
  		includeStackTrace: true,
  		defaultTimeoutInterval: 30000
	}
};
