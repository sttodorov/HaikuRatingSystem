(function () {
    'use strict';

    function data($http, $q, authorization, notifier, baseServiceUrl) {

        function get(url, queryParams) {
            var defered = $q.defer();

            $http.get(baseServiceUrl + '/' + url, { params: queryParams})
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function post(url, postData, askForCode) {
            var defered = $q.defer();

            if (askForCode) {
                var authHeader = authorization.getAuthorizationHeader();
            }

            $http.post(baseServiceUrl + '/' + url, postData, { headers: authHeader })
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function put() {
            var authHeader = authorization.getAuthorizationHeader();

            throw new Error('Not implemented!');
        }
        function deleteData() {
            var authHeader = authorization.getAuthorizationHeader();

            throw new Error('Not implemented!');
        }

        function getErrorMessage(response) {
            return response.data.Message;
        }

        return {
            get: get,
            post: post,
            put: put
        };
    }

    angular.module('HaikusRatingSystem.services')
        .factory('data', ['$http', '$q', 'authorization', 'notifier', 'baseServiceUrl', data]);
}());