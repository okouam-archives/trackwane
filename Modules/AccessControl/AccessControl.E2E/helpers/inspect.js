var pp = require('jsome');

module.exports = function(result) {
	if (result.response) {
		console.log("===================== REQUEST =============================");
		pp(result.response.request);
		console.log("===================== RESPONSE STATUS ===========================");
		pp({
			statusCode: result.response.statusCode,
			statusMessage: result.response.statusMessage
		});
		console.log("===================== RESPONSE HEADERS ===========================");
		pp(result.response.headers);
		if (result.body) {
			console.log("===================== RESPONSE BDOY ===========================");
			pp(result.body);
		}
	}
}
