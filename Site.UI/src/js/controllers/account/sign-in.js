angular.module('app.controllers')
    .controller('accountSignInController', ['$scope', '$state', '$form', '$auth', ($scope, $state, $form, $auth) => {

        $scope.model = {};

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                return $auth.signIn($scope.model.userName, $scope.model.password)
                    .then(() => {

                        if ($state.params.returnUrl)
                            $state.go($state.params.returnUrl);
                        else
                            $state.go('account/index');

                    }, (error) => {

                        debugger
                        $scope.status = error.status;
                        $scope.description = error.data.error_description;
                    })
                    .finally(() => { $scope.isBusy = false });
            });
        };
    }]);