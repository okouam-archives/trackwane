module.exports = function() {
	this.pp = require('jsome');
	﻿this.chakram = require('chakram');
	this.sleep = require('./helpers/sleep');
	this.inspect = require('./helpers/inspect');
	this.API = require('./helpers/api');
	this.defaults = require('./helpers/defaults');
	this.expect = this.chakram.expect;
	this._ = require('lodash');
	var Hashids = require("hashids");
	this.hashids = new Hashids("this is my salt");
}
