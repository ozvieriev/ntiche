angular.module('app.controllers')
    .controller('accountController', ['$scope', '$state', '$api', '$auth', ($scope, $state, $api, $auth) => {

        //return $state.go('exam/feedback');

        if ($auth.isInRoles(['admin']))
            return $state.go('admin/report');

        $scope.model = {};

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = true;
        $scope.view = null;

        $api.examResults().then(
            (response) => {

                $scope.status = 200;
                $scope.model.examResults = response;

                let isAnyPreTest = $scope.model.examResults.filter((item) => {
                    return item.examName === 'pre-test';
                }).length > 0;

                let isAnyPostTest = $scope.model.examResults.filter((item) => {
                    return item.examName === 'post-test';
                }).length > 0;

                let isAnySuccessPostTest = $scope.model.examResults.filter((item) => {
                    return item.examName === 'post-test' && item.isSuccess
                }).length > 0;

                /*--------------------------------------------------------------------*/
                //isAnySuccessPostTest = true

                isAnyPreTest = false;
                isAnySuccessPostTest = false
                /*--------------------------------------------------------------------*/

                if (!isAnyPreTest && !isAnyPostTest)
                    return ($scope.view = 'question');

                if (isAnySuccessPostTest)
                    return ($scope.view = 'download-certificate')

                return $state.go('exam/post-test');
            },
            (error) => {

                $scope.status = error.status;
                $scope.description = error.data.error_description;
            })
            .finally(() => { $scope.isBusy = false })

    }]);