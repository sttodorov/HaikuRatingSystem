(function () {
    'use strict';

    function config($routeProvider, $locationProvider) {

        var PARTIALS_PREFIX = 'views/partials/';
        var CONTROLLER_AS_VIEW_MODEL = 'vm';

        $locationProvider.html5Mode(true);

        $routeProvider
            .when('/', {
                templateUrl: PARTIALS_PREFIX + 'home/home.html',
                controller: 'HomeController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/register', {
                templateUrl: PARTIALS_PREFIX + 'identity/register.html',
                controller: 'RegisterController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/haikus', {
                templateUrl: PARTIALS_PREFIX + 'haikus/haikus.html',
                controller: 'HaikusController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/reported', {
                templateUrl: PARTIALS_PREFIX + 'haikus/reportedHaikus.html',
                controller: 'ReportedHaikusController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/haikus/:id', {
                templateUrl: PARTIALS_PREFIX + 'haikus/haikuDetails.html',
                controller: 'HaikuDetailsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/users', {
                templateUrl: PARTIALS_PREFIX + 'users/users.html',
                controller: 'UsersController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/users/:username', {
                templateUrl: PARTIALS_PREFIX + 'users/userDetails.html',
                controller: 'UserViewModel',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/:username/haikus', {
                templateUrl: PARTIALS_PREFIX + 'haikus/publishHaiku.html',
                controller: 'HaikusPublishController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .otherwise({ redirectTo: '/' });
    }

    angular.module('HaikusRatingSystem.services', []);
    angular.module('HaikusRatingSystem.directives', []);
    angular.module('HaikusRatingSystem.controllers', ['HaikusRatingSystem.services']);
    angular.module('HaikusRatingSystem', ['ngRoute', 'ngCookies', 'HaikusRatingSystem.controllers', 'HaikusRatingSystem.directives'])
        .config(['$routeProvider', '$locationProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', 'http://localhost:17031');
}());