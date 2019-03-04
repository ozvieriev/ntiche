angular.module('app.controllers')
    .controller('accountSignInController', ['$scope', '$auth', ($scope, $auth) => {

        $scope.signIn = () => {

            $auth.signIn();
        };
    }]);