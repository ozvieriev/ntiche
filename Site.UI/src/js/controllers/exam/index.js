angular.module('app.controllers')
    .controller('examController', ['$scope', '$state', '$api', '$timeout', ($scope, $state, $api, $timeout) => {

        $scope.model = {};

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = true;
        $scope.view = null;

        $api.accoutnActivity().then((response) => {

            $scope.isBusy = false;

            if (!response.totalPreTests) {

                $scope.view = 'question';
                return;
            }

            $state.go('exam/post-test');
        });

        let timer = $timeout(() => {

            if ($scope.isBusy)
                $scope.view = 'loading';

        }, 1000);

        $scope.$on('$destroy', () => {

            $timeout.cancel(timer);
        });
    }]);