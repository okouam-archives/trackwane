(function() {
	angular
		.module("trackwane")
		.component('dashboardLocations', {
			controller: [
				"services.applicationState",
				function($applicationState) {
					this.locations = $applicationState.locations;
					this.state_icon = "vertical_align_top";
					this.state_class = "collapsed"
					this.toggle = function() {
						if (this.state_icon == "vertical_align_top") {
							this.state_icon = "vertical_align_bottom"
							this.state_class = "expanded"
						} else {
							this.state_icon = "vertical_align_top";
							this.state_class = "collapsed"
						}
					}
				}
			],
			template: (
				<div>
					<ul>
						<li class="header">Locations ({{$ctrl.locations.length}})
							<a href="#"><i style="float: right" class="material-icons" ng-click="$ctrl.toggle()">{{$ctrl.state_icon}}</i></a>
							<a href="#"><i style="float: right; margin-right: 10px" class="material-icons">add_box</i></a>
						</li>
						<li style="cursor: pointer" ng-class="$ctrl.state_class" class="entry" ng-repeat="location in $ctrl.locations">
							<i class="material-icons">directions_bus</i>
							<span class="identifier">{{location.name}}</span>
							<p>{{location.location}}</p>
						</li>
					</ul>
				</div>

			)
		});
})();
