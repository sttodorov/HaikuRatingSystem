(function () {
    'use strict';

    function data($http, $q, authorization, notifier, baseServiceUrl) {

        function get(url, queryParams) {
            var defered = $q.defer();

            $http.get(baseServiceUrl + '/' + url, { params: queryParams })
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

        function put(url, putData) {
            var defered = $q.defer();
            var authHeader = authorization.getAuthorizationHeader();

            $http.put(baseServiceUrl + '/' + url, putData, { headers: authHeader })
                 .then(function (response) {
                     defered.resolve(response.data);
                 }, function (error) {
                     error = getErrorMessage(error);
                     notifier.error(error);
                     defered.reject(error);
                 });

            return defered.promise;
        }

        function deleteData(url) {
            var defered = $q.defer();
            var authHeader = authorization.getAuthorizationHeader();

            $http.delete(baseServiceUrl + '/' + url, { headers: authHeader })
                 .then(function (response) {
                     defered.resolve(response.data);
                 }, function (error) {
                     error = getErrorMessage(error);
                     notifier.error(error);
                     defered.reject(error);
                 });
            return defered.promise;
        }

        function getErrorMessage(response) {
            return response.data.Message || response.data;
        }

        return {
            get: get,
            post: post,
            put: put,
            deleteData: deleteData
        };
    }

    angular.module('HaikusRatingSystem.services')
        .factory('data', ['$http', '$q', 'authorization', 'notifier', 'baseServiceUrl', data]);
}());