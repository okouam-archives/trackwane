(function () {
    "use strict";

    angular
        .module("trackwane")
        .controller("data.tracker-list.main.controller", controller);

    controller.$inject = ["$scope", "data.tracker-list.store"];

    function controller($scope, Store) {

        activate();

        function activate() {
            $scope.trackers = Store.findAllTrackers();
        }
    }
})();
﻿
