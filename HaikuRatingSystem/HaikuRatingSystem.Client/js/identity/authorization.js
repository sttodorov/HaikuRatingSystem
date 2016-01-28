(function () {
    'use strict';

    function authorization(identity) {
        return {
            getAuthorizationHeader: function () {

                // TODO: Export this in partial pop up 
                var publishCode = prompt("Enter publish code");

                return {
                    'PublishCode': publishCode
                }
            }
        }
    }

    angular.module('HaikusRatingSystem.services')
        .factory('authorization', ['identity', authorization]);
}());