angular.module('app.controllers')
    .controller('accountEmailConfirmationController', ['$scope', '$auth', '$utils', ($scope, $auth, $utils) => {

        $scope.status = null;
        $scope.isBusy = false;

        let query = $utils.query();

        if (!query.accountid || !query.emailconfirmationtoken)
            return ($scope.status = 404);

            debugger
        $auth.emailConfirmation(query.emailconfirmationtoken, query.accountid);
    }]);