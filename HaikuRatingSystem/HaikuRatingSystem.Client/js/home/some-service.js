(function () {
    'use strict';

    function StatisticsService(data) {

        function getStats() {
            return data.get('api/stats');
        }

        return {
            getStats: getStats
        }
    }

    angular.module('myApp.services')
        .factory('StatisticsService', ['data', StatisticsService]);
}());