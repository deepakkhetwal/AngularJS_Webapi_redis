var homeModule = angular.module('homeModule', ['ngRoute', 'userControllers', 'userService', 'userRoleControllers'
	]);
homeModule.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
		when('/', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-list.html', controller: 'userCtrl' })
		.when('/admn/user', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-list.html', controller: 'userCtrl' })
        .when('/admn/user-create', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-create.html', controller: 'userCreateCtrl' })
        .when('/admn/user-create-offline', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-create-offline.html', controller: 'userOfflineCreateCtrl' })
        .when('/admn/user-role-list', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-role-list.html', controller: 'userRoleCtrl' })
        .when('/admn/user-offline-sync', { templateUrl: global.WEBSITE_URL + '/js/views/admn/user/user-offline-sync.html', controller: 'userOfflineSyncCtrl' });
        
}])
.factory('$exceptionHandler', function () {
    return function (exception, cause) {
        alert(exception.message);
        exception.message += ' (caused by "' + cause + '")';

    };
})
.directive('usernameValidator', ['userService', function (userService) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.userId = function (modelValue, viewValue) {
                var keyInJson = JSON.parse('{"key":"' + viewValue + '"}');
                return userService.IsUserExists.query(keyInJson).$promise.then(
                    function (response) {

                        if (response.UserExists) {

                            return false;
                        }
                        return true;
                    }
                );
            };
        }
    };
}])
;
