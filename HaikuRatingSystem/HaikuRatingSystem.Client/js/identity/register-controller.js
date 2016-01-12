(function () {
    'use strict';

    function RegisterController($location, authService, notifier) {
        var vm = this;

        vm.register = function (user) {
            authService.registerUser(user).then(function () {
                notifier.success('Registration successful!');
                $location.path('/');
            })
        };
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('RegisterController', ['$location', 'AuthenticationService', 'notifier', RegisterController]);
}());