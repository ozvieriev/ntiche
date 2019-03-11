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
        service.examResults = () => {

            return $http.get(_uri('api/exam/results'), {
                asJson: true
            });
        };
        service.examPost = (name, answers) => {

            return $http.post(_uri(`api/exam/${name}`),
                answers,
                { asJson: true });
        };

        return service;
    }]);