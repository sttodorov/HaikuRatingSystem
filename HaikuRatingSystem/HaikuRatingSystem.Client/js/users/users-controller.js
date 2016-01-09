(function () {
    'use strict';

    function UsersController(usersService) {
        var vm = this;

        vm.request = {
            sorttype : 0,
            sortby : 0,
            page: 1,
            take: 10
        };

        vm.prevPage = function () {
            if (vm.request.page == 1) {
                return;
            }

            vm.request.page--;
            vm.request.skip = (vm.request.page - 1) * vm.request.take;
            vm.filterUsers();
        }

        vm.nextPage = function () {
            if (!vm.users || vm.users.length == 0) {
                return;
            }

            vm.request.page++;
            vm.request.skip = (vm.request.page - 1) * vm.request.take;
            vm.filterUsers();
        }

        vm.filterUsers = function () {
            usersService.getUsers(vm.request)
                .then(function (filteredUsers) {
                    vm.users = filteredUsers;
                });
        };

        vm.filterUsers();
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('UsersController', ['UsersService', UsersController]);
}());