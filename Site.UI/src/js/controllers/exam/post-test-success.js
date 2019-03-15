angular.module('app.controllers')
    .controller('examPostTestSuccessController', ['$scope', '$state', '$api', '$translate',
        ($scope, $state, $api, $translate) => {

            let params = $state.params;
            if (!params.examResultId)
                return $state.go('exam');

            $scope.model = {};

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = true;

            $scope.download = () => {

                let queryString = $.param({
                    examResultId: params.examResultId,
                    lang: $translate.use()
                });

                window.open(`/api/exam/post-test/download/?${queryString}`, '_blank');
            };
            $scope.send = () => {

            };
        }]);