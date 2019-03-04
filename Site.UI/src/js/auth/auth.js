angular.module('app.auth')
    .factory('$auth', ['$q', '$state', '$injector', '$authStorage', ($q, $state, $injector, $authStorage) => {


        var _apiHeaders = { 'Content-Type': 'application/x-www-form-urlencoded' };
        var service = {};

        var _isAuthenticated = false;

        service.isAuthenticated = () => {

            return _isAuthenticated;
        };
        service.signIn = () => {

            _isAuthenticated = true;

            if ($state.params.returnUrl)
                $state.go($state.params.returnUrl);
            else
                $state.go('index');
        };

        return service;
    }]);