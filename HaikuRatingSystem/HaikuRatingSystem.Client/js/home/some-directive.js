(function () {
    'use strict';

    function allTrips() {
        return {
            retrict: 'AC',
            templateUrl: 'views/directives/some-directive.html'
        }
    }

    angular.module('myApp.directives')
        .factory('allTrips', [allTrips]);
}());