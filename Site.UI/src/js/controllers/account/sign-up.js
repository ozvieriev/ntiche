angular.module('app.controllers')
    .controller('accountSignUpController', ['$scope', '$auth', ($scope, $auth) => {

        $auth.accountGet({ email: 'ozvieriev@gmail.com' })
            .then(
                (response) => { return { isEmailExists: true }; },
                (error) => {

                    return $auth.accountGet({ userName: 'MegaZver22' })
                        .then(
                            () => { return { isUserNameExists: true } },
                            () => { return {}; })
                })
            .then((response) => {

                if (response.isEmailExists) {

                    //show error notification   
                    //$scope.error = 'User with email address already exists';
                }

                if (response.isUserNameExists) {

                    //show error notification
                    //$scope.error = 'User with user name already exists';
                }
            });

    }]);