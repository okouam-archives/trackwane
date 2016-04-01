(function () {
    'use strict';

    angular
        .module('trackwane')
        .factory('data.tracker-list.actions', actions);

    actions.$inject = ['ngReflux'];

    function actions(ngReflux) {
		return ngReflux.createActions([]);
    }
})();
