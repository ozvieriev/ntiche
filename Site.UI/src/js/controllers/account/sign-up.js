angular.module('app.controllers')
    .controller('accountSignUpController', ['$scope', '$state', '$form', '$auth', ($scope, $state, $form, $auth) => {

        $scope.model = {
            //userName: 'MegaZver',
            //password: 'anatole64',
            //firstName: 'Oleksandr',
            //lastName: 'Zvieriev',
            //email: 'ozvieriev@gmail.com',
            //ocupation: 'Web Developer',
            //city: 'Montreal',
            //isOptin: true
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                return $auth.signUp(angular.copy($scope.model))
                    .then(
                        () => {

                            $auth.signIn($scope.model.userName, $scope.model.password)
                                .then(() => {

                                    if ($state.params.returnUrl)
                                        $state.go($state.params.returnUrl);
                                    else
                                        $state.go('account/index');
                                });

                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };

    }]);