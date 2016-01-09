(function () {
    'use strict';

    function allUsers() {
        return {
            restrict: 'A',
            templateUrl: 'views/directives/all-users-directive.html'
        }
    }

    angular.module('HaikusRatingSystem.directives')
        .directive('allUsers', [allUsers]);
}());