(function () {
    'use strict';

    function allHaikus() {
        return {
            restrict: 'A',
            templateUrl: 'views/directives/all-haikus-directive.html'
        }
    }

    angular.module('HaikusRatingSystem.directives')
        .directive('allHaikus', [allHaikus]);
}());