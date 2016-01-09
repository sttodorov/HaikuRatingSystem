(function () {
    'use strict';

    function UsersService(data) {

        function getUsers(filters) {
            return data.get('api/users', filters);
        };

        function getUser(username) {
            return data.get('api/users/' + username);
        };

        return {
            getUsers: getUsers,
            getUser: getUser
        }
    }

    angular.module('HaikusRatingSystem.services')
        .factory('UsersService', ['data', UsersService]);
}());