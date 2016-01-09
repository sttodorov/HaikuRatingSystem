(function () {
    'use strict';

    function UserViewModel(usersService, $routeParams) {
        var vm = this;

        vm.getUser = function (username) {
            usersService.getUser(username).then(function (data) { vm.user = data; })
        }
        vm.getUser($routeParams.username);
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('UserViewModel', ['UsersService', '$routeParams', UserViewModel]);
}());