angular.module('app.controllers')
    .controller('supportController', ['$scope', ($scope) => { 

        $scope.host = location.host;

    }]);