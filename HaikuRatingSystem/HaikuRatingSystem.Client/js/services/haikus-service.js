(function () {
    'use strict';

    function HaikusService(data) {

        function getHaikus(filters) {
            return data.get('api/haikus', filters);
        };

        return {
            getHaikus: getHaikus
        };
    };

    angular.module('HaikusRatingSystem.services')
        .factory('HaikusService', ['data', HaikusService]);
}());