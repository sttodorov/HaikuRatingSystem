﻿(function () {
    'use strict';

    function HaikusController(haikusService) {
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
            vm.filterHaikus();
        }

        vm.nextPage = function () {
            if (!vm.haikus || vm.haikus.length == 0) {
                return;
            }

            vm.request.page++;
            vm.request.skip = (vm.request.page - 1) * vm.request.take;
            vm.filterHaikus();
        }

        vm.filterHaikus = function () {
            haikusService.getHaikus(vm.request)
                .then(function (filteredHaikus) {
                    vm.haikus = filteredHaikus;
                });
        };

        vm.filterHaikus();
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('HaikusController', ['HaikusService', HaikusController]);
}());