angular.module('app.controllers')
    .controller('partialHeaderController', ['$scope', '$state', '$auth', '$translate', ($scope, $state, $auth, $translate) => {

        $scope.$auth = $auth;

        $scope.currentLanguage = $state.params.locale;
        $scope.languages = {
            en: { icon: 'flag-icon-gb' },
            fr: { icon: 'flag-icon-fr' }
        };

        delete $scope.languages[$scope.currentLanguage];

        $scope.changeLanguage = (lang) => {

            $translate.use(lang).then(() => {

                let url = $state.current.url.replace(/^\//g, '');
                let params = angular.copy($state.params);
                params.locale = lang;

                $state.go(url, params);
            });
        };
        $scope.logout = () => {

            $auth.logout();
        };
    }]);