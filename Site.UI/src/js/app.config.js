angular.module('app')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push(['$q', '$state', ($q, $state) => {

            let interceptor = {};

            interceptor.request = function (config) {

                if ($state.params && $state.params.locale)
                    config.headers['Accept-Language'] = $state.params.locale;

                return config;
            };

            interceptor.response = (response) => {

                let config = response.config || {};

                if (config.asJson === true)
                    return response.data;

                return response;
            };
            interceptor.responseError = (response) => {

                if (response.config.asJson === true) {

                    response.data = response.data || {};

                    if (!response.data.error_description && !response.status === 401)
                        $state.go('error');

                    response.data.error_description = response.data.error_description || 'Sorry, the server is busy. Please try again later.';
                }

                return $q.reject(response);
            };

            return interceptor;
        }]);
    }]);