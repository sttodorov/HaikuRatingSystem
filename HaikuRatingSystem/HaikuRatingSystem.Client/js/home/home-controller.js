(function () {
    'use strict';

    function HomeController(StatisticsService) {
        var vm = this;

        StatisticsService.getStats()
            .then(function (stats) {
                vm.stats = stats;
            });

        vm.home = 'Home';
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['StatisticsService', HomeController]);
}()); 