angular.module('app.controllers')
    .controller('adminReportController', ['$scope', '$auth', '$state', ($scope, $auth, $state) => {

        $scope.model = {
            accountFrom: null,
            accountTo: null
        };

        let _getModel = () => {

            let model = angular.copy($scope.model);

            let from = Date.parse($scope.model.accountFrom);
            let to = Date.parse($scope.model.accountTo);

            delete model.accountFrom;
            delete model.accountTo;

            from && (model.accountFrom = from/1000);
            to && (model.accountTo = to / 1000);

            return model;
        };

        $scope.downloadExamReport = () => {

            window.open(`/api/exam/report/?${$.param(_getModel())}`, '_blank');
        };
        $scope.downloadEvaluationReport = () => {

            window.open(`/api/feedback/report/?${$.param(_getModel())}`, '_blank');
        };
    }]);