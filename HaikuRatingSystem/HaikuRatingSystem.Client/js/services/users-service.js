(function () {
    'use strict';

    function UsersService(data) {

        function getUsers(filters) {
            return data.get('api/users', filters);
        };

        function getUser(username) {
            return data.get('api/users/' + username);
        };

        function promoteUser(username) {
            console.log("Promoting " + username);
            return data.put('api/users/promote/' + username, username, true);
        };

        function deleteUser(username) {
            return data.deleteData('api/users/' + username);
        };
        function deleteAllHaikus(username) {
            return data.deleteData('api/' + username + '/haikus');
        };

        return {
            getUsers: getUsers,
            getUser: getUser,
            deleteUser: deleteUser,
            promoteUser: promoteUser,
            deleteAllHaikus: deleteAllHaikus
        }
    }

    angular.module('HaikusRatingSystem.services')
        .factory('UsersService', ['data', UsersService]);
}());