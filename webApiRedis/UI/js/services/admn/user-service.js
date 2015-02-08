var userService = angular.module('userService', ['ngResource']);
userService.factory('userService', ['$resource',
	function ($resource) {
	    return {
	        GetUserList: $resource(global.WEBAPI_URL + '/users/getuser', {}, { query: { method: 'GET', isArray: true } }),
	        CreateUser: $resource(global.WEBAPI_URL + '/users/postuser', {}, { save: { method: 'POST', headers: { 'Content-Type': 'application/json' } } }),
	        UpdateUser: $resource(global.WEBAPI_URL + '/users/putuser', {}, { save: { method: 'PUT', headers: { 'Content-Type': 'application/json' } } }),
	        SyncUser: $resource(global.WEBAPI_URL + '/users/syncuser', {}, { save: { method: 'POST', headers: { 'Content-Type': 'application/json' } } }),
	        DeleteUser: $resource(global.WEBAPI_URL + '/users/deleteuser/:key', { key: '@p_Key' }, { delete_user: { method: 'DELETE' } }),
	        IsUserExists: $resource(global.WEBAPI_URL + '/users/isuserexists/:key', { key: '@p_Key' }, { query: { method: 'GET' } }),
	        GetUserRoleList: $resource(global.WEBAPI_URL + '/roles/getrole', {}, { query: { method: 'GET', isArray: true } })
	    };

	}]);