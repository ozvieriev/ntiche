angular.module('app.controllers')
    .controller('accountEmailConfirmationController', ['$scope', '$auth', '$utils', ($scope, $auth, $utils) => {

        $scope.status = null;
        $scope.isBusy = false;

        let query = $utils.query();

        if (!query.token || !query.account)
            return ($scope.status = 404);

        $auth.emailConfirmation(query.token, query.account);
    }]);