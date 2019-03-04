angular.module('app.auth')
    .factory('$auth', ['$state', '$injector', '$authStorage', ($state, $injector, $authStorage) => {


        let _apiHeaders = { 'Content-Type': 'application/x-www-form-urlencoded' };
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
        service.emailConfirmation = (emailConfirmationToken, accountId) => {

            let data = {
                grant_type: 'password',
                emailConfirmationToken: emailConfirmationToken,
                accountId: accountId
            };

            debugger   
            let $http = $injector.get("$http");

            $http.post(_uri('api/token'),
                $.param(data), {
                    headers: _apiHeaders
                });
        };

        return service;
    }]);