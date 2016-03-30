(function () {
    'use strict';

    angular
        .module('trackwane')
        .factory('Data.TrackerList.TrackerProvider', factory);

    factory.$inject = [];

    function factory() {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();
