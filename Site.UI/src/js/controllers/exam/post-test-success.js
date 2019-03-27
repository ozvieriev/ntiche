angular.module('app.controllers')
    .controller('examPostTestSuccessController', ['$scope', '$state', '$api', '$form', '$translate',
        ($scope, $state, $api, $form, $translate) => {

            let params = $state.params;
            if (!params.examResultId)
                return $state.go('exam');

            $scope.model = {};

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            $scope.download = () => {

                let queryString = $.param({
                    examResultId: params.examResultId,
                    lang: $translate.use()
                });

                window.open(`/api/exam/post-test/download/?${queryString}`, '_blank');
            };
            $scope.submit = (form) => {

                $form.submit($scope, form, () => {

                    return $api.examPostTestSend({ examResultId: params.examResultId })
                        .then(
                            (response) => {

                                $scope.status = 200;
                                $scope.description = response.description;
                            },
                            (error) => {

                                $scope.status = error.status;
                                $scope.description = error.data.error_description;
                            })
                        .finally(() => { $scope.isBusy = false; });
                });
            };
        }]);