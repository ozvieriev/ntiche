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
        service.examPost = (answers) => {

            return $http.post(_uri('api/exam'),
                answers,
                { asJson: true });
        };

        return service;
    }]);