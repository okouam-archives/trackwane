(function() {
	angular
		.module("trackwane")
		.component('dashboardTrackers', {
			template: (
				<div>
					<ul>
						<dashboard-trackers-header />
					    <dashboard-trackers-rows />
					</ul>
				</div>
			)
		});
})();
