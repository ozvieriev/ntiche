angular.module('app.controllers')
    .controller('accountSignUpController', ['$scope', '$form', '$auth', ($scope, $form, $auth) => {

        $scope.model = {
            userName: 'MegaZver',
            password: 'anatole64',
            firstName: 'Oleksandr',
            lastName: 'Zvieriev',
            email: 'ozvieriev@gmail.com',
            ocupation: 'Web Developer',
            city: 'Montreal',
            isOptin: true
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                return $auth.signUp(angular.copy($scope.model))
                    .then(
                        (response) => {

                            $scope.status = 200;
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