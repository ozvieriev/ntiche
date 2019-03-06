angular.module('app.controllers')
    .controller('accountResetPasswordController', ['$scope', '$state', '$form', '$auth',
        ($scope, $state, $form, $auth) => {

            $scope.model = {};

            var params = $state.params;

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            if (!params.resetPasswordToken || !params.accountId) {

                $scope.status = 404;
                $scope.description = 'Reset password link is incorrect!';
                return;
            }
            $scope.submit = (form) => {

                $form.submit($scope, form, () => {

                    return $auth.resetPassword(params.resetPasswordToken, params.accountId, $scope.model.password)
                        .then(
                            (response) => {

                                $scope.status = 200;
                                $scope.description = response.description;
                            },
                            (error) => {

                                $scope.status = error.status;
                                $scope.description = error.data.error_description;
                            })
                        .finally(() => { $scope.isBusy = false });
                });
            };

        }]);