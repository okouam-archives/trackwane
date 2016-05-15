(function() {
	angular
		.module("trackwane")
		.component('dashboardTrackers', {
			template: (
				<div>
					<ul class="collapsible popout">
						<dashboard-trackers-header />
					    <dashboard-trackers-rows />
					</ul>
				</div>
			)
		});
})();
