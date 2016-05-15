(function() {

	angular
		.module("trackwane")
		.service('services.dashboard.mapService', [
			function() {
				this.moveTo = (coords) => {
					console.log(coords);
				}
			}
		]);
})();
