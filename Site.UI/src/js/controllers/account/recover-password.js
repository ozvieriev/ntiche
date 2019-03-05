angular.module('app.controllers')
    .controller('accountRecoverPasswordController', ['$scope', '$state', '$form', '$auth',
        ($scope, $state, $form, $auth) => {

            $scope.model = {};

            $scope.status = null;
            $scope.isBusy = false;

            $scope.submit = (form) => {

                $scope.status = null;
                $form.submit($scope, form, () => {

                    return $auth.recoverPassword({ email: $scope.model.email })
                        .then(() => { $scope.status = 200; }, (error) => {

                            $scope.status = error.status;

                            if (error.status !== 404)
                                return $state.go('error');
                        })
                        .finally(() => { $scope.isBusy = false });
                });
            };

        }]);