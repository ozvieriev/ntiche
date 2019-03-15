angular
    .module('app.services')
    .factory('$api', ['$http', function factory($http) {

        let _uri = (method) => {

            return `/${method}`;
        };

        var service = {};

        service.accoutnActivity = () => {

            return $http.get(_uri('api/account/activity'), {
                asJson: true
            });
        };
        service.examPost = (name, answers) => {

            return $http.post(_uri(`api/exam/${name}`),
                answers,
                { asJson: true });
        };
        service.examPostTestSend = (data) => {

            return $http.post(_uri(`api/exam/post-test/send`),
                data,
                { asJson: true });
        };
        service.feedbackPost = (data) => {

            return $http.post(_uri('api/feedback'),
                data,
                { asJson: true });
        };

        return service;
    }]);