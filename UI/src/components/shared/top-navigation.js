(function() {
	angular
		.module("trackwane")
		.component('topNavigation', {
			controller: function() {
			},
			template: (
				<div class="wrapper">
					<h4>Trackwane</h4>
					<ul>
					    <li><a ui-sref="dashboard">Dashboard</a></li>
						<li class="sep">|</li>
						<li><a ui-sref="timelines">Timelines</a></li>
						<li class="sep">|</li>
						<li><a ui-sref="reports">Reports</a></li>
						<li class="sep">|</li>
						<li><a ui-sref="notifications">Notifications</a></li>
						<li class="sep">|</li>
						<li><a ui-sref="access-control">Access Control</a></li>
					</ul>
					<ul style="float: right; margin-right: 30px; margin-top: 25px">
						Olivier Kouame
						<li><i class="material-icons" style="position: relative; top: 5px">person</i></li>
						<li>Sign Out</li>
					</ul>
				</div>
			)
		});
})();
