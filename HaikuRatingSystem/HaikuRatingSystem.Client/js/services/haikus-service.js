(function () {
    'use strict';

    function HaikusService(data) {

        function getHaikus(filters) {
            return data.get('api/haikus', filters);
        };

        function getHaikuById(id) {
            return data.get('api/haikus/' + id);
        };

        function publishHaiku(username, haiku) {
            return data.post('api/' + username + '/haikus', haiku, true);
        }

        function rateHaiku(id, rating) {
            return data.post('api/haikus/' + id, { 'RatingValue': rating });
        }

        function editHaiku(haiku) {
            return data.put('api/' + haiku.AuthorName + '/haikus/' + haiku.Id, { 'Text': haiku.Text });
        }

        function deleteHaiku(haiku) {
            return data.deleteData('api/' + haiku.AuthorName + '/haikus/' + haiku.Id);
        }

        return {
            getHaikus: getHaikus,
            publishHaiku: publishHaiku,
            getHaikuById: getHaikuById,
            rateHaiku: rateHaiku,
            editHaiku: editHaiku,
            deleteHaiku: deleteHaiku
        };
    };

    angular.module('HaikusRatingSystem.services')
        .factory('HaikusService', ['data', HaikusService]);
}());