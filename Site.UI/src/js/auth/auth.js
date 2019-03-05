angular.module('app.auth')
    .factory('$auth', ['$q', '$injector', '$authStorage', ($q, $injector, $authStorage) => {

        let _apiHeaders = { 'Content-Type': 'application/x-www-form-urlencoded' };
        let _uri = (method) => {

            return `http://localhost:37321/${method}`;
        };

        let service = {};

        service.isAuthenticated = () => {

            return !!$authStorage.getItem();
        };
        service.signIn = (userName, password) => {

            let data = {
                grant_type: 'password',
                userName: userName,
                password: password
            };

            let deferred = $q.defer();
            let $http = $injector.get("$http");

            $http.post(_uri('api/token'), $.param(data), { headers: _apiHeaders, asJson: true })
                .then((json) => {

                    $authStorage.setItem(json);
                    deferred.resolve(json);

                }, (error) => {
                    deferred.reject(error.data);
                });

            return deferred.promise;
        };
        service.accountGet = (params) => {

            let $http = $injector.get("$http");

            return $http.get(_uri('api/account'), {
                params: params,
                asJson: true
            });
        };
        service.emailConfirmation = (token, account) => {

            let data = {
                grant_type: 'password',
                emailConfirmationToken: token,
                accountId: account
            };

            let $http = $injector.get("$http");

            $http.post(_uri('api/token'),
                $.param(data), {
                    headers: _apiHeaders
                });
        };
        service.recoverPassword = (params) => {

            let $http = $injector.get("$http");

            return $http.get(_uri('api/account/recover-password'), {
                params: params,
                asJson: true
            });
        };
        service.resetPassword = (token, account, password) => {

            let $http = $injector.get("$http");

            return $http.post(_uri('api/account/reset-password'),
                {
                    resetPasswordToken: token,
                    accountId: account,
                    password: password
                },
                { asJson: true });
        };


        return service;
    }]);