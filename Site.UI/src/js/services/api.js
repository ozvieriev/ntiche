angular
    .module('app.services')
    .factory('$api', ['$http', '$state', function factory($http, $state) {

        let _uri = (method) => {

            return `/${method}`;
        };

        var service = {};

        service.exam = (name) => {

            return $http.get(_uri('api/exam'), {
                params: { name: name || 'drugs' },
                asJson: true
            });
        };

        return service;
    }]);