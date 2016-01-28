(function () {
    'use strict';

    function HaikusService(data) {

        function getHaikus(filters) {
            return data.get('api/haikus', filters);
        };

        function publishHaiku(username, haiku) {
            return data.post('api/' + username + '/haikus', haiku, true);
        }

        return {
            getHaikus: getHaikus,
            publishHaiku: publishHaiku
        };
    };

    angular.module('HaikusRatingSystem.services')
        .factory('HaikusService', ['data', HaikusService]);
}());