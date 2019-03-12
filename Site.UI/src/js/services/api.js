angular
    .module('app.services')
    .factory('$api', ['$http', '$state', function factory($http, $state) {

        let _uri = (method) => {

            return `/${method}`;
        };

        var service = {};

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
        service.feedbackPost = (data) => {

            return $http.post(_uri('api/feedback'),
                data,
                { asJson: true });
        };

        return service;
    }]);