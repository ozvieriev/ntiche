angular.module('app.controllers')
    .controller('adminReportController', ['$scope', '$auth', '$state', ($scope, $auth, $state) => {

        let tomorrow = new Date();
        tomorrow.setDate(new Date().getDate() + 1);

        var accountTo =
            ('0' + (tomorrow.getMonth() + 1)).slice(-2) + '/'
            + ('0' + tomorrow.getDate()).slice(-2) + '/'
            + tomorrow.getFullYear();

        $scope.model = {
            accountFrom: '3/1/2019',
            accountTo: accountTo
        };

        let _getModel = () => {

            let model = angular.copy($scope.model);

            let from = Date.parse($scope.model.accountFrom);
            let to = Date.parse($scope.model.accountTo);

            delete model.accountFrom;
            delete model.accountTo;

            from && (model.accountFrom = from / 1000);
            to && (model.accountTo = to / 1000);

            return model;
        };

        $scope.downloadExamReport = () => {

            window.open(`/api/exam/report/?${$.param(_getModel())}`, '_blank');
        };
        $scope.downloadEvaluationReport = () => {

            window.open(`/api/feedback/report/?${$.param(_getModel())}`, '_blank');
        };
        $scope.downloadReports = () => {

            $scope.downloadExamReport();
            $scope.downloadEvaluationReport();
        };

        $scope.$watch('pharmacySetting', newValue => {

            $scope.model.accountPharmacySettingId = newValue.id;
        });
    }]);