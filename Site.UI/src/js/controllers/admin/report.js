angular.module('app.controllers')
    .controller('adminReportController', ['$scope', '$auth', '$state', ($scope, $auth, $state) => {

        $scope.model = {};
        
        $scope.downloadExamReport = () => {

            
        };
        $scope.downloadEvaluationReport = () => {

            window.open(`/api/feedback/report/?${$.param($scope.model)}`, '_blank');
        };
    }]);