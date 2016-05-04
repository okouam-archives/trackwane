var pp = require('jsome');
﻿var chakram = require('chakram');
var expect = chakram.expect;
var Hashids = require("hashids"), hashids = new Hashids("this is my salt");

describe("Register Application Command", function() {

    it("should register a new application", function () {
		chakram.setRequestDefaults({
			headers: {
				"X-Trackwane-Application": hashids.encode(new Date().getTime())
			}
		});
        return chakram.post("http://api.trackwane.local/application", {
			name: "afsdf"
		}).then(function(result) {
			console.log("===================== REQUEST =============================")
			pp(result.response.request);
			console.log("===================== RESPONSE HEADERS ===========================")
			pp(result.response.headers);
			console.log("===================== RESPONSE BDOY ===========================")
			pp(result.response.body);
			console.log("===================== RESPONSE HEADERS ===========================")
		});
    });

});
