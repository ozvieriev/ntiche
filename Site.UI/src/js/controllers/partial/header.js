angular.module('app.controllers')
    .controller('partialHeaderController', ['$scope', '$state', ($scope, $state) => {

        $scope.changeLanguage = (lang) => {

            var url = $state.current.url.replace(/^\//g, '');
            var params = angular.copy($state.params);
            params.locale = lang;

            $state.go(url, params);
        };
    }]);