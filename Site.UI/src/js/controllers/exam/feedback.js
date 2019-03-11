angular.module('app.controllers')
    .controller('examFeedbackController', ['$scope', '$sce', '$form', '$api', ($scope, $sce, $form, $api) => {

        $scope.model = {};

        $scope.getRatings = () => {

            return [1, 2, 3, 4, 5];
        };
        $scope.getRatingsYesNo = () => {

            return [
                { text: 'Yes', value: true },
                { text: 'No', value: false },
            ]
        };
        $scope.getProgramRatings = () => {

            return [
                { text: 'Poor', value: 1 },
                { text: 'Below Average', value: 2 },
                { text: 'Average', value: 3 },
                { text: 'Above Average', value: 4 },
                { text: 'Excellent', value: 5 },
            ];
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                debugger;
                return $api.feedbackPost($scope.model)
                    .then(
                        (response) => {

                            $scope.status = 200;
                            $scope.description = response.description;
                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };
    }]);