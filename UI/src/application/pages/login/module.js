import angular from 'angular'
import Controller from './controller'

const MODULE_NAME = 'trackwane.application.pages.login';

angular
	.module(MODULE_NAME, [])
	.controller('application.pages.login.controller', Controller);

export default MODULE_NAME
