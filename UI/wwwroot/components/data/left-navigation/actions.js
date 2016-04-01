(function() {

	angular
		.app('trackwane')
		.factory('data.left-navigation.actions', actions);

	actions.$inject = ['ngReflux'];

	function actions(ngReflux) {
	    return ngReflux.createActions([
	        'changePage'
	    ]);
	}
});
