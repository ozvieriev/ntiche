angular.module('app.auth')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push(['$q', '$auth', '$authInterceptor', '$authBuffer', ($q, $auth, $authInterceptor, $authBuffer) => {

            var interceptor = {};

            interceptor.request = (config) => {

                config.headers = config.headers || {};

                let accessToken = $auth.accessToken();

                accessToken && (config.headers.Authorization = `bearer ${accessToken}`);

                return config;
            };
            interceptor.responseError = (rejection) => {

                var config = rejection.config || {};

                switch (rejection.status) {

                    case 401:
                        var deferred = $q.defer();
                        $authBuffer.append(config, deferred);
                        $auth.refreshToken().then($authInterceptor.loginConfirmed, $auth.logout);

                        return deferred.promise;
                }

                return $q.reject(rejection);
            }

            return interceptor;
        }]);
    }]);