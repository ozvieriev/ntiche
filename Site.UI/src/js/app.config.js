angular.module('app')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push(['$q', '$translate', '$state', ($q, $translate, $state) => {

            let interceptor = {};

            interceptor.request = function (config) {

                config.headers['Accept-Language'] = $translate.use();

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

                    if (!response.data.error_description)
                        $state.go('error');

                    response.data.error_description = response.data.error_description || 'Sorry, the server is busy. Please try again later.';
                }

                return $q.reject(response);
            };

            return interceptor;
        }]);
    }]);