(function() {
	angular
		.module("trackwane")
		.component('scheduledReports', {
			controller: function() {
				this.scheduled_reports = [
					{
						name: "Patrick's Monthly Speed Report A",
						type: 'speed',
						trackers: [{organization: 'ssdfsfd', key: '3dxXed3'}],
						frequency: 'weekly',
						recipients: ['sdfsdf@sfsdf.com'],
						startsOn: '2017-02-01'
					}
				];
			},
			template: (
				<div style="width: 100%">
					<ul>
						<li class="header">Scheduled Reports</li>
						<li ng-repeat="report in $ctrl.scheduled_reports" style="padding: 10px">
							<i class="material-icons" style="position: relative; top: 5px">directions_bus</i>
							{{report.name}}
						</li>
					</ul>
				</div>
			)
		});
})();
