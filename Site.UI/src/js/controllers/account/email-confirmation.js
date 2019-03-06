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

        $auth.emailConfirmation(params.emailConfirmationToken, params.accountId).then(
            (response) => {

                $scope.status = 200;
                $scope.description = 'Your account has been activated.';
            },
            (error) => {

                $scope.status = error.status;
                $scope.description = error.data.error_description;
            })
            .finally(() => { $scope.isBusy = false; });
    }]);