var sleep = require('./sleep');

module.exports = function(host, chakram) {

	function verify(result, skipVerification, expectedCode) {
		if (!skipVerification) chakram.expect(result).to.have.status(expectedCode || 200);
		return result;
	}

	this.save = function(applicationKey, organizationKey, reading, skipVerification) {
		return chakram
			.post(host + "/data/" + applicationKey + "/" + organizationKey, reading, {})
			.then(function(result) {
				return verify(result, skipVerification, 204);
			});
	};
}
