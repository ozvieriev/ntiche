angular.module('app.controllers')
    .controller('accountController', ['$scope', '$state', ($scope, $state) => {

        let params = $state.params;
       
        $scope.model = {
            newsletterLink: `/pdf/newsletter-${params.locale}.pdf`
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = true;
    }]);