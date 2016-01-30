(function () {
    'use strict';

    function HaikuDetailsController(haikusService, $routeParams, $location) {
        var vm = this;

        vm.getHaiku = function (id) {
            haikusService.getHaikuById(id).then(function (data) { console.log(data); vm.haiku = data; })
        }

        vm.rateHaiku = function () {
            haikusService.rateHaiku(vm.haiku.Id, vm.rating).then(function (data) {
                $location.path('/haikus');
            });
        }

        vm.editHaiku = function () {
            haikusService.editHaiku(vm.haiku).then(function (data) {
                $location.path('/haikus');
            });
        }
        vm.deleteHaiku = function () {
            haikusService.deleteHaiku(vm.haiku).then(function (data) {
                $location.path('/haikus');
            });
        }

        vm.getHaiku($routeParams.id);
    }

    angular.module('HaikusRatingSystem.controllers')
        .controller('HaikuDetailsController', ['HaikusService', '$routeParams', '$location', HaikuDetailsController]);
}());