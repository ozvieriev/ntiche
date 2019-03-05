angular.module('app.controllers')
    .controller('accountSignInController', ['$scope', '$state', '$form', '$auth', ($scope, $state, $form, $auth) => {

        $scope.model = {
            error: null
        };

        $scope.status = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $scope.model.error = null;
            $form.submit($scope, form, () => {

                return $auth.signIn($scope.model.userName, $scope.model.password)
                    .then(() => {

                        if ($state.params.returnUrl)
                            $state.go($state.params.returnUrl);
                        else
                            $state.go('account/index');

                    }, (json) => {

                        $scope.model.error = json.error_description;
                    })
                    .finally(() => { $scope.isBusy = false });
            });
        };
    }]);