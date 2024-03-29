angular.module('app.auth')
    .factory('$auth', ['$q', '$injector', '$authStorage', '$state', ($q, $injector, $authStorage, $state) => {

        let _apiHeaders = { 'Content-Type': 'application/x-www-form-urlencoded' };
        let _uri = (method) => {

            return `/${method}`;
        };

        let service = {};

        service.isAuthenticated = () => {

            return !!service.identity();
        };
        service.identity = () => {

            return $authStorage.getItem();
        };
        service.accessToken = () => {

            let identity = service.identity();

            return identity ? identity.access_token : null;
        };
        service.isInRoles = (roles) => {

            var identity = service.identity();

            if (roles && !identity)
                return false;

            if (!roles.length)
                return true;

            var identityRoles = (identity.roles || '')
                .toLowerCase()
                .split(',');

            for (var index = 0; index < roles.length; index++) {

                let role = roles[index];
                if (identityRoles.indexOf(role) !== -1)
                    return true;
            }

            return false;
        };


        service.signUp = (data) => {

            let $http = $injector.get("$http");

            data.countryIso2 = 'CA';

            return $http.post(_uri('api/account/register'),
                data,
                { asJson: true });
        };
        service.signIn = (email, password, emailConfirmationToken, accountId) => {

            let data = {
                grant_type: 'password'
            };

            email && (data.userName = email);
            password && (data.password = password);
            emailConfirmationToken && (data.emailConfirmationToken = emailConfirmationToken);
            accountId && (data.accountId = accountId);

            let deferred = $q.defer();
            let $http = $injector.get("$http");

            $http.post(_uri('api/token'),
                $.param(data), {
                    headers: _apiHeaders,
                    asJson: true
                })
                .then((json) => {

                    $authStorage.setItem(json);
                    deferred.resolve(json);

                }, (error) => { deferred.reject(error); });

            return deferred.promise;
        };

        service.refreshToken = () => {

            var deferred = $q.defer();

            let identity = service.identity();

            if (identity) {

                var data = {
                    grant_type: 'refresh_token',
                    refresh_token: identity.refresh_token
                };

                let $http = $injector.get("$http");
                $http.post(_uri('api/token'), $.param(data), {
                    headers: _apiHeaders,
                    asJson: true
                })
                    .then((json) => {

                        $authStorage.setItem(json);
                        deferred.resolve(json);

                    }, (error) => { debugger; deferred.reject(); });
            }
            else
                deferred.reject();

            return deferred.promise;
        };
        service.logout = () => {

            $authStorage.removeItem();
            $state.go('account/sign-in');
        };
        service.emailConfirmation = (emailConfirmationToken, accountId) => {

            return service.signIn(null, null, emailConfirmationToken, accountId);
        };
        service.recoverPassword = (params) => {

            let $http = $injector.get("$http");

            return $http.get(_uri('api/account/recover-password'), {
                params: params,
                asJson: true
            });
        };
        service.resetPassword = (resetPasswordToken, accountId, password) => {

            let $http = $injector.get("$http");

            return $http.post(_uri('api/account/reset-password'),
                {
                    resetPasswordToken: resetPasswordToken,
                    accountId: accountId,
                    password: password
                },
                { asJson: true });
        };


        return service;
    }]);