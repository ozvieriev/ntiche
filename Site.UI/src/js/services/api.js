angular
    .module('app.services')
    .factory('$api', ['$http', function factory($http) {

        let _uri = (method) => {

            return `/${method}`;
        };

        var service = {};

        service.exam = () => {

            return $http.get(_uri('api/exam'), {
                asJson: true
            });
        };

        return service;
    }]);