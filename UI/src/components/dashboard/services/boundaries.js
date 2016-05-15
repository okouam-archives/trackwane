(function() {
	angular
		.module("trackwane")
		.component('dashboardBoundaries', {
			controller: [
				"services.applicationState",
				function($applicationState) {
					this.boundaries = $applicationState.boundaries;
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
						<li class="header">Boundaries ({{$ctrl.boundaries.length}})
						<a href="#"><i style="float: right" class="material-icons" ng-click="$ctrl.toggle()">{{$ctrl.state_icon}}</i></a>
						<a href="#"><i style="float: right; margin-right: 10px" class="material-icons">add_box</i></a>
						</li>
					</ul>
				</div>

			)
		});
})();
 
