angular.module('app.controllers')
    .controller('examController', ['$scope', '$state', '$api', '$timeout', ($scope, $state, $api, $timeout) => {

        let _isDestroyed = false;

        $scope.model = {};

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = true;
        $scope.view = null;

        $api.accoutnActivity().then((response) => {

            $scope.isBusy = false;
            if (response.bestPostExamResultIsSuccess && response.bestPostExamResultId) {

                if (response.totalFeedbacks)
                    return $state.go(`exam/post-test-success`, { examResultId: response.bestPostExamResultId });

                return $state.go(`exam/feedback`, { examResultId: response.bestPostExamResultId });
            }
            if (!response.totalPreTests) {

                $scope.view = 'question';
                return;
            }

            $state.go('exam/post-test');
        }, () => {

            if (_isDestroyed)
                return;

            $state.go('error');
        });

        let timer = $timeout(() => {

            if ($scope.isBusy)
                $scope.view = 'loading';

        }, 1000);

        $scope.$on('$destroy', () => {

            _isDestroyed = true;

            $timeout.cancel(timer);
        });
    }]);