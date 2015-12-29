(function () {
    'use strict';

    function UsersService(data) {

        function getUsers() {
            return data.get('api/users');
        }

        return {
            getUsers: getUsers
        }
    }

    angular.module('myApp.services')
        .factory('UsersService', ['data', UsersService]);
}());