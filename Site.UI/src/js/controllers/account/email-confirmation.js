angular.module('app.controllers')
    .controller('accountEmailConfirmationController', ['$scope', '$auth', '$state', ($scope, $auth, $state) => {

        var params = $state.params;

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        if (!params.emailConfirmationToken || !params.accountId) {

            $scope.status = 404;
            $scope.description = 'Email confirmation link is incorrect!';
            return;
        }

        $auth.emailConfirmation(params.emailConfirmationToken, params.accountId);
    }]);