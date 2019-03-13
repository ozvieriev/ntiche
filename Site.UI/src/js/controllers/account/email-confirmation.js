angular.module('app.controllers')
    .controller('accountEmailConfirmationController', ['$scope', '$auth', '$state', '$timeout', ($scope, $auth, $state, $timeout) => {

        let params = $state.params;

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;
        $scope.view = null;

        if (!params.emailConfirmationToken || !params.accountId) {

            $scope.status = 404;
            $scope.description = 'Email confirmation link is incorrect!';
            return;
        }

        $scope.isBusy = true;
        $auth.emailConfirmation(params.emailConfirmationToken, params.accountId).then(
            (response) => {

                $scope.status = 200;
                $scope.description = 'Your account has been activated.';
            },
            (error) => {

                $scope.status = error.status;
                $scope.description = error.data.error_description;
            })
            .finally(() => {

                $scope.isBusy = false;
                $scope.view = 'done';
            });

        let timer = $timeout(() => {

            if ($scope.isBusy)
                $scope.view = 'loading';

        }, 1000);

        $scope.$on('$destroy', () => {

            $timeout.cancel(timer);
        });
    }]);