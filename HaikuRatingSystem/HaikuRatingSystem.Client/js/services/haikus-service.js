(function () {
    'use strict';

    function HaikusService(data) {

        function getHaikus() {
            return data.get('api/haikus');
        }

        return {
            getHaikus: getHaikus
        }
    }

    angular.module('myApp.services')
        .factory('HaikusService', ['data', HaikusService]);
}());