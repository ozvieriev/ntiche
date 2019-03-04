angular.module('app.auth')
    .factory('$authBuffer', ['$injector', ($injector) => {

        /** Holds all the requests, so they can be re-requested in future. */
        var _buffer = [];

        /** Service initialized later because of circular dependency problem. */
        var $http;

        var _retryHttpRequest = (config, deferred) => {

            var _success = (response) => {
                deferred.resolve(response);
            };
            var _error = (response) => {
                deferred.reject(response);
            };

            $http = $http || $injector.get('$http');
            $http(config).then(_success, _error);
        }

        var service = {};

        /**
        * Appends HTTP request configuration object with deferred response attached to buffer.
        */
        service.append = (config, deferred) => {

            _buffer.push({
                config: config,
                deferred: deferred
            });
        };

        /**
        * Abandon or reject (if reason provided) all the buffered requests.
        */
        service.rejectAll = (reason) => {

            if (reason) {

                for (var index = 0; index < _buffer.length; ++index)
                    _buffer[index].deferred.reject(reason);
            }

            _buffer = [];
        };

        /**
         * Retries all the buffered requests clears the buffer.
         */
        service.retryAll = (updater) => {

            for (var index = 0; index < _buffer.length; ++index)
                _retryHttpRequest(updater(_buffer[index].config), _buffer[index].deferred);

            _buffer = [];
        }

        return service;
    }]);