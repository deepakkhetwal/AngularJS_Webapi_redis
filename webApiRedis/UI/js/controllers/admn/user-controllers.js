var userControllers = angular.module('userControllers', []);

userControllers.controller('userCtrl', ['$scope', 'userService', function ($scope, userService) {
    // $scope.init();
    $scope.isEdit = false;
    $scope.userList = userService.GetUserList.query();
   
    $scope.updateUser = function (key, id, firstName, lastName, roleKey) {
        //this.isEdit = false;
        var objThis = this;
        var user = JSON.stringify({ Key: key, Id: id, FirstName: firstName, LastName: lastName, RoleKey: roleKey});
        var d = userService.UpdateUser.save(user);
        d.$promise.then(function (user) {
            objThis.isEdit = false;
        });
    };
    $scope.deleteUser = function (key) {
        var objThis = this;
        var keyInJson = JSON.parse('{"key":"' + key + '"}'); // double quotes were needed as key contains ":" like person:103
        var d = userService.DeleteUser.delete(keyInJson);
        d.$promise.then(function (user) {
            objThis.isEdit = false;
            $scope.userList = userService.GetUserList.query();
        });
    }
    
}]);
userControllers.controller('userCreateCtrl', ['$scope', 'userService', function ($scope, userService) {
    $scope.roleList = userService.GetUserRoleList.query();
    $scope.submit = function () {
        
        var user = JSON.stringify({Id : $scope.Id, FirstName : $scope.FirstName, LastName : $scope.LastName, RoleKey:$scope.selectedRole.Key});
        var d = userService.CreateUser.save(user);
        d.$promise.then(function (user) {
            $scope.Id = "";
            $scope.FirstName = "";
            $scope.LastName = "";
            COMMON.showGlobalMessageAlert("user created successfully");
        }, function (error) {
            var db = COMMON.openOfflineConn();
            db.users.add({ Key: "person_" + $scope.Id, Id: $scope.Id, FirstName: $scope.FirstName, LastName: $scope.LastName, RoleKey: $scope.selectedRole.Key });
            db.close();
        });

    };
}]);

userControllers.controller('userOfflineCreateCtrl', ['$scope', 'userService', function ($scope, userService) {
    $scope.roleList = userService.GetUserRoleList.query();
    $scope.submit = function () {
        // store offline
        var db = COMMON.openOfflineConn();
        db.users.add({ Key: "person_" + $scope.Id, Id: $scope.Id, FirstName: $scope.FirstName, LastName: $scope.LastName, RoleKey: $scope.selectedRole.Key });
        db.close();
        $scope.Id = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        COMMON.showGlobalMessageAlert("user created offline successfully");

    };
}]);

userControllers.controller('userOfflineSyncCtrl', ['$scope', 'userService', function ($scope, userService) {
    $scope.selection = [];
    listOfflineUser();
    $scope.toggleSelection = function toggleSelection(userId)
    {
        var idx = $scope.selection.indexOf(userId);
        if(idx > -1)
        {
            $scope.selection.splice(idx, 1);
        }
        else
        {
            $scope.selection.push(userId);    
        }
    }

    $scope.synchronize = function synchronize()
    {
        var db = COMMON.openOfflineConn();
        db.users.where("Id").anyOf($scope.selection).toArray().then(function(result)
        {
            
            var user = JSON.stringify(result);
            var d = userService.SyncUser.save(user);
            d.$promise.then(function (data) {
                
                for(var i=0 ; i< result.length ; i++)
                {
                    db.users.where("Id").equals(result[i].Id).delete();
                }
                listOfflineUser();

            });
        });
        db.close();

    }
    $scope.deleteOfflineUserInfo = function deleteOfflineUserInfo(userId)
    {
        var db = COMMON.openOfflineConn();
        db.users.where("Id").equals(userId).delete().then(listOfflineUser);
        db.close();
    }

    function listOfflineUser()
    {
        var db = COMMON.openOfflineConn();
        db.users.toArray().then(function (result) {
            
            $scope.$apply(function () {
                
                $scope.userList = result; 
            });
          
        });
        db.close();
    }
    
  
}]);
