angular.module('app')
    .config(['$httpProvider', ($httpProvider) => {

        $httpProvider.interceptors.push([() => {

            let interceptor = {};

            interceptor.response = (response) => {

                let config = response.config || {};

                if (config.asJson === true)
                    return response.data;

                return response;
            };

            return interceptor;
        }]);
    }]);