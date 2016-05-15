(function() {

	angular
		.module("trackwane")
		.component('dashboardPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row; display: flex; flex: 1">
						<div style="overflow-y: scroll">
							<dashboard-trackers />
						</div>
						<dashboard-map />
						<dashboard-tracker class="z-depth-5" />
						<dashboard-actions class="z-depth-5" />
					</div>
				</div>
			)
		});
})();
