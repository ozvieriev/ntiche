angular.module('app.controllers')
    .controller('accountEmailConfirmationController', ['$scope', '$utils', ($scope, $utils) => {

        $scope.status = null;
        $scope.isBusy = false;

        let query = $utils.query();

        if (!query.accountid || !query.emailconfirmationtoken)
            return ($scope.status = 404);

    }]);