(function () {
    'use strict';

    function auth($http, $q, identity, authorization, baseServiceUrl) {
        var usersApi = baseServiceUrl + '/api/users'

        return {
            signup: function (user) {
                var deferred = $q.defer();

                $http.post(usersApi , user)
                    .then(function () {
                        deferred.resolve();
                    }, function (response) {
                        deferred.reject(response.message);
                    });

                return deferred.promise;
            },
            isAuthenticated: function () {
                if (identity.isAuthenticated()) {
                    return true;
                }
                else {
                    return $q.reject('not authorized');
                }
            }
        }
    }

    angular.module('HaikusRatingSystem.services')
        .factory('auth', ['$http', '$q', 'identity', 'authorization', 'baseServiceUrl', auth]);
}());