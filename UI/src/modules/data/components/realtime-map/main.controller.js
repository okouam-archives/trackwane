(function () {
    'use strict';

    angular
        .module('trackwane')
        .controller('data.realtime-map.main.controller', controller);

    controller.$inject = ['$scope'];

    function controller($scope) {

        activate();

        function activate() {
			$scope.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 };
        }
    }
})();
