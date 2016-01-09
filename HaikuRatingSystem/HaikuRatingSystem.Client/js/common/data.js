(function () {
    'use strict';

    function data($http, $q, authorization, notifier, baseServiceUrl) {

        function get(url, queryParams) {
            var defered = $q.defer();

            var authHeader = authorization.getAuthorizationHeader();
            console.log("Request url: " + url);
            console.log("Request params: ");
            console.log(queryParams);

            $http.get(baseServiceUrl + '/' + url, { params: queryParams, headers: authHeader })
                .then(function (response) {
                    defered.resolve(response.data);
                }, function (error) {
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function post(url, postData) {
            var defered = $q.defer();

            var authHeader = authorization.getAuthorizationHeader();

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

        // TODO: Implement PUT and DELETE, auth required for both
        function put() {
            var authHeader = authorization.getAuthorizationHeader();

            throw new Error('Not implemented!');
        }
        function deleteData() {
            var authHeader = authorization.getAuthorizationHeader();

            throw new Error('Not implemented!');
        }

        function getErrorMessage(response) {
            var error = response.data.modelState;
            if (error && error[Object.keys(error)[0]][0]) {
                error = error[Object.keys(error)[0]][0];
            }
            else {
                error = response.data.message;
            }

            return error;
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