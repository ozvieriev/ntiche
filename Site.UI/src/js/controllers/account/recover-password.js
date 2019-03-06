angular.module('app.controllers')
    .controller('accountRecoverPasswordController', ['$scope', '$form', '$auth',
        ($scope, $form, $auth) => {

            $scope.model = {};

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            $scope.submit = (form) => {

                $form.submit($scope, form, () => {

                    return $auth.recoverPassword({ email: $scope.model.email })
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