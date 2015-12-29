(function () {
    'use strict';

    function HomeController(UsersService) {
        var vm = this;

        UsersService.getUsers()
            .then(function (users) {
                vm.users = users;
            });

        vm.home = 'Home';
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['UsersService', HomeController]);
}()); 