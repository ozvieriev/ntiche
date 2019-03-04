angular.module('app.auth')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push(['$q', '$auth', '$authInterceptor', '$authBuffer', ($q, $auth, $authInterceptor, $authBuffer) => {

            var interceptor = {};

            interceptor.request = (config) => {

                config.headers = config.headers || {};

                //   if ($auth.isAuthenticated())
                //       config.headers.Authorization = 'bearer ' + $auth.getAccessToken();

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