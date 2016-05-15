(function() {

	angular
		.module("trackwane")
		.component('reportsPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row; display: flex; flex: 1">
						<div style="display: flex; flex: 1; flex-direction: column; max-width: 350px">
							<report-builder />
							<saved-reports />
							<scheduled-reports />
						</div>
						<report-tracker-picker  style="display: flex; flex: 1; max-width: 300px" />
						<report-viewer style="display: flex; flex: 1" />
						<report-actions class="z-depth-5" style="right: 10px; top: 85px;  z-index: 20000; background-color: #333; width: 210px; border-top: 1px #666 solid" />
					</div>
				</div>
			)
		});
})();
