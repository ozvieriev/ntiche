angular.module('app.controllers')
    .controller('accountResetPasswordController', ['$scope', '$state', '$form', '$utils', '$auth',
        ($scope, $state, $form, $utils, $auth) => {

            $scope.model = {};

            $scope.status = null;
            $scope.isBusy = false;

            let query = $utils.query();

            if (!query.token || !query.account)
                return ($scope.status = 404);

            $scope.submit = (form) => {

                $scope.status = null;
                $form.submit($scope, form, () => {

                    // return $auth.recoverPassword({ email: $scope.model.email })
                    //     .then(() => { $scope.status = 200; }, (error) => {

                    //         $scope.status = error.status;

                    //         if (error.status !== 404)
                    //             return $state.go('error');
                    //     })
                    //     .finally(() => { $scope.isBusy = false });
                });
            };

        }]);