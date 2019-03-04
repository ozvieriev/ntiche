angular.module('app.auth')
    .factory('$authStorage', ['$window', ($window) => {

        var _localStorage = {};

        var _getItem = (key) => {

            var json = _localStorage[key];
            var value = json ? JSON.parse(json) : null;

            _localStorage[key] = json;

            try {
                json = $window.localStorage.getItem(key);
                json && (value = JSON.parse(json));
            }
            catch (error) { console.error(error); }

            return value;
        };
        var _setItem = (key, value) => {

            var json = JSON.stringify(value);

            _localStorage[key] = json;

            try { $window.localStorage.setItem(key, json); }
            catch (error) { console.error(error); }
        };
        var _removeItem = (key) => {

            delete _localStorage[key];

            try { $window.localStorage.removeItem(key); }
            catch (error) { console.error(error); }

        };

        var service = {};

        service.getItem = (brandIso) => {

            return _getItem(`auth:${brandIso}`);
        };
        service.setItem = (brandIso, json) => {

            _setItem(`auth:${brandIso}`, json);
        };
        service.removeItem = (brandIso) => {

            _removeItem(`auth:${brandIso}`);
        };

        return service;
    }]);