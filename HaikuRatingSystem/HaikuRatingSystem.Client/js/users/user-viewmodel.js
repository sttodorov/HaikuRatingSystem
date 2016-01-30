(function () {
    'use strict';

    function UserViewModel(usersService, $routeParams, $location, $route) {
        var vm = this;

        vm.getUser = function (username) {
            usersService.getUser(username).then(function (data) { vm.user = data; })
        }
        vm.promoteUser = function () {
            usersService.promoteUser(vm.user.Username).then(function (data) {
                $location.path("/users");
            });
        };
        vm.deleteUser = function () {
            usersService.deleteUser(vm.user.Username).then(function (data) {
                $location.path('/users');
            });
        };
        vm.deleteAllHaikus = function () {
            usersService.deleteAllHaikus(vm.user.Username).then(function (data) {
                $route.reload();
            });
        };

        vm.getUser($routeParams.username);
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('UserViewModel', ['UsersService', '$routeParams', '$location', '$route', UserViewModel]);
}());