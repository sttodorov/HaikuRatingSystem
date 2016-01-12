(function () {
    'use strict';

    function AuthenticationService(data) {

        function registerUser(user) {
            return data.post('api/users', user);
        };

        return {
            registerUser: registerUser
        };
    };

    angular.module('HaikusRatingSystem.services')
        .factory('AuthenticationService', ['data', AuthenticationService]);
}());