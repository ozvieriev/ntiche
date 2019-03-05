angular.module('app.auth')
    .factory('$auth', ['$q', '$state', '$injector', '$authStorage', ($q, $state, $injector, $authStorage) => {


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
        service.emailConfirmation = (emailConfirmationToken, accountId) => {

            let data = {
                grant_type: 'password',
                emailConfirmationToken: emailConfirmationToken,
                accountId: accountId
            };

            let $http = $injector.get("$http");

            $http.post(_uri('api/token'),
                $.param(data), {
                    headers: _apiHeaders
                });
        };
        service.recoverPassword = (params)=>{

            let $http = $injector.get("$http");
            return $http.get(_uri('api/account/recover-password'), {
                params: params,
                asJson: true
            });
        };

        return service;
    }]);