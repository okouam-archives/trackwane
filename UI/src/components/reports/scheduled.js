(function() {
	angular
		.module("trackwane")
		.component('scheduledReports', {
			controller: function() {
				this.scheduled_reports = [
					{
						name: "Monthly Speed Report",
						type: 'speed',
						trackers: [{organization: 'ssdfsfd', key: '3dxXed3'}],
						frequency: 'weekly',
						recipients: ['sdfsdf@sfsdf.com'],
						startsOn: '2017-02-01'
					}
				];
			},
			template: (
				<div>
					SCHEDULED REPORTS
				</div>
			)
		});
})();
