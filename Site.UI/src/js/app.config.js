angular.module('app')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push(['$q', '$state', ($q, $state) => {

            let interceptor = {};

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