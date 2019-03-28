angular.module('app.controllers')
    .controller('examPostTestQuestionController', ['$scope', '$api', '$form',
        ($scope, $api, $form) => {

            $scope.model = {};

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            $scope.submit = (form) => {

                $form.submit($scope, form, () => {

                    let model = angular.copy($scope.model);

                    return $api.examPostTestQuestion(model)
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