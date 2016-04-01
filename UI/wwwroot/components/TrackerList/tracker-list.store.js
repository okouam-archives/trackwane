(function () {
    'use strict';

    angular
        .module('trackwane')
        .factory('Data.TrackerList.Store', factory);

    factory.$inject = ['ngReflux', 'Data.TrackerList.Actions'];

    function factory(ngReflux, actions) {
		return ngReflux.createStore({
			listenables: [actions],
			init: function() {
				this.trackers = [
	                { hardwareId: "7ce0b8e9-e1c0-45e1-a9f7-549d224d82c0", key: "KVO9yy1oO5j", model: "sdfsd", isArchived: false, identifier: "N16 GKM" },
	                { hardwareId: "b8e7c097-b7c3-4f8c-a6e8-0546df34f6af", key: "aBMswoO2UB3Sj", model: "sdfsd", isArchived: false, identifier: "X40 JHD" },
	                { hardwareId: "02c11068-eccc-4b7e-85d9-8ae7827b6267", key: "gB0NV05e", model: "sdfsd", isArchived: true, identifier: "L999 JMS" },
	                { hardwareId: "12d96b48-1596-47c8-b6ed-b77f7508f478", key: "kRNrpKlJ", model: "sdfsd", isArchived: false, identifier: "JD05 CPD" }
	            ];
			},
			findAllTrackers: function() {
				return this.trackers;
			}
		});
    }
})();
