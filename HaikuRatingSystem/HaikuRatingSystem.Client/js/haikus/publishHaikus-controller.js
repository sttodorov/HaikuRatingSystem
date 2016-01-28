(function () {
    'use strict';

    function HaikusPublishController(haikusService, $routeParams, $location, notifier) {
        var vm = this;

        vm.publishHaiku = function (haiku) {
            haikusService.publishHaiku($routeParams.username, haiku).then(function (data, err) {
                notifier.success('Published successfully!');

                // TODO: redirect to newly created haiku page -> $routePrams.username + '/haikus/' + data.id
                $location.path('/haikus');
            });
        };
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('HaikusPublishController', ['HaikusService', '$routeParams', '$location', 'notifier', HaikusPublishController]);
}());