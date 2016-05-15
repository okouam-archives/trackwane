(function() {
	angular
		.module("trackwane")
		.component('dashboardActions', {
			controller: function() {
			},
			template: (
				<div>
					<p style="padding: 25px; font-weight: bold; font-size: 18px; padding-bottom: 0; padding-top: 0">Actions</p>
					<div style="padding: 25px; padding-top: 5px">
						<p>List Locations</p>
						<p>List Trackers</p>
						<p>List Boundaries</p>
						<p>Show/Hide Locations</p>
						<p>Show/Hide Boundaries</p>
						<p>Show/Hide Trackers</p>
						<p>Add New Tracker</p>
						<p>Add New Location</p>
						<p>Add New Boundary</p>
					</div>
				</div>
			)
		});
})();
