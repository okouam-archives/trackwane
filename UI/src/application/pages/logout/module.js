import angular from 'angular'
import Controller from './controller'

const MODULE_NAME = 'trackwane.application.pages.logout';

angular
	.module(MODULE_NAME, [])
	.controller('application.pages.logout.controller', Controller);

export default MODULE_NAME
