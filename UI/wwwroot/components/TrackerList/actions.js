(function () {
    'use strict';

    angular
        .module('trackwane')
        .factory('Data.TrackerList.Actions', actions);

    actions.$inject = ['ngReflux'];

    function actions(ngReflux) {
		return ngReflux.createActions([]);
    }
})();
