angular.module('app.controllers')
    .controller('adminReportController', ['$scope', '$auth', '$state', ($scope, $auth, $state) => {

        $scope.model = {};
        
        $scope.downloadExamReport = () => {

            window.open(`/api/exam/report/?${$.param($scope.model)}`, '_blank');
        };
        $scope.downloadEvaluationReport = () => {

            window.open(`/api/feedback/report/?${$.param($scope.model)}`, '_blank');
        };
    }]);