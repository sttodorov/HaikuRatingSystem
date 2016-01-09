(function () {
    'use strict';

    function HomeController(UsersService, HaikusService) {
        var vm = this;

        UsersService.getUsers()
            .then(function (users) {
                vm.users = users;
            });

        HaikusService.getHaikus()
            .then(function (haikus) {
                vm.haikus = haikus;
            });
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('HomeController', ['UsersService', 'HaikusService', HomeController]);
}()); 