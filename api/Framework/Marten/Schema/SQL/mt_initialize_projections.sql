CREATE OR REPLACE FUNCTION {databaseSchema}.mt_initialize_projections(overwrite boolean) RETURNS VOID AS $$

if (plv8.transforms && !overwrite){
	return;
}

var $modules = {};
var module = {};
var exports = null;

function require(name){
	var parts = name.split('/');
	name = parts[parts.length - 1].replace(".js", "");


	if ($modules.hasOwnProperty(name)){
		return $modules[name];
	}

	module = {exports: {}};
	exports = module.exports;

	var raw = plv8.execute("select definition from {databaseSchema}.mt_modules where name = $1", [name])[0].definition;
	try {
		eval(raw);
	}
	catch (err){
		throw 'Failed to evaluate the module ' + name + '\n' + err;
	}
	

	var newModule = module.exports;
	$modules[name] = newModule;

	return newModule;
}

var console = {
	log: function(text){
		plv8.elog(NOTICE, text);
	}
}

var transforms = require('mt_transforms');

var results = plv8.execute("select definition from  {databaseSchema}.mt_projections");

for (var i = 0; i < results.length; i++){
	eval(results[i].definition);
}


plv8.transforms = transforms;




$$ LANGUAGE plv8;


CREATE OR REPLACE FUNCTION {databaseSchema}.mt_get_projection_usage() RETURNS JSON AS $$

	if (!plv8.transforms){
		plv8.execute('select {databaseSchema}.mt_initialize_projections(true)');
	}

	return plv8.transforms.usages();
		

$$ LANGUAGE plv8;


SET plv8.start_proc = plv8_initialize;