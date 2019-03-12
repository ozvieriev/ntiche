angular.module('app.controllers')
    .controller('partialHeaderController', ['$scope', '$state', '$auth', '$translate', ($scope, $state, $auth, $translate) => {

        $scope.$auth = $auth;

        $scope.changeLanguage = (lang) => {

            $translate.use(lang).then(() => {

                var url = $state.current.url.replace(/^\//g, '');
                var params = angular.copy($state.params);
                params.locale = lang;

                $state.go(url, params);
            });
        };
        $scope.logout = () => {

            $auth.logout();
        };
    }]);