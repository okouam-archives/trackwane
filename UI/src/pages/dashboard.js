(function() {

	angular
		.module("trackwane")
		.component('dashboardPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row; display: flex; flex: 1">
						<dashboard-trackers style="overflow-y: scroll" />
						<dashboard-map />
						<dashboard-tracker class="z-depth-5" style="display: none; right: 10px; top: 85px;  z-index: 20000; background-color: #333; width: 300px; border-top: 1px #666 solid" />
						<dashboard-actions class="z-depth-5" style="right: 10px; top: 85px;  z-index: 20000; background-color: #333; width: 210px; border-top: 1px #666 solid" />
					</div>
				</div>
			)
		});
})();
