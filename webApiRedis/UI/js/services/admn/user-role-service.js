var userRoleService = angular.module('userRoleService', ['ngResource']);
userRoleService.factory('userRoleService', ['$resource',
	function ($resource) {
	    return {
	        GetUserRoleList: $resource(global.WEBAPI_URL + '/roles/getrole', {}, { query: { method: 'GET', isArray: true } })
	       
	    };

	}]);