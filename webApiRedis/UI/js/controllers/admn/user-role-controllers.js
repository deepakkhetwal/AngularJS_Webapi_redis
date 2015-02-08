var userRoleControllers = angular.module('userRoleControllers', []);

userRoleControllers.controller('userRoleCtrl', ['$scope', 'userService', function ($scope, userService) {
    
    $scope.userRoleList = userService.GetUserRoleList.query();
   
    
}]);
