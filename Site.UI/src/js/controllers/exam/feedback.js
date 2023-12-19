angular.module('app.controllers')
    .controller('examFeedbackController', ['$scope', '$form', '$api', '$state',
        ($scope, $form, $api, $state) => {

            let params = $state.params;
            if (!params.examResultId)
                return $state.go('exam');

            $scope.model = {};

            $scope.getRatings = (max) => {

                max = max || 4;

                var ratings = [];
                for (var index = 0; index < max; index++)
                    ratings.push(index);

                return ratings;
            };
            $scope.getRatingsYesNo = () => {

                return [
                    { text: 'Yes', value: true },
                    { text: 'No', value: false }
                ]
            };
            $scope.getProgramRatings = () => {

                return [
                    { text: 'Poor', value: 0 },
                    { text: 'Satisfactory', value: 1 },
                    { text: 'Very Good', value: 2 },
                    { text: 'Excellent', value: 3 }
                ];
            };

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            $scope.submit = (form) => {

                $form.submit($scope, form, () => {

                    return $api.feedbackPost($scope.model)
                        .then(
                            (response) => {

                                return $state.go(`exam/post-test-success`, { examResultId: params.examResultId });
                            },
                            (error) => {

                                $scope.status = error.status;
                                $scope.description = error.data.error_description;
                            })
                        .finally(() => { $scope.isBusy = false });
                });
            };
        }]);