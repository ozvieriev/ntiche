angular.module('app.auth')
    .factory('$authStorage', ['$window', ($window) => {

        let _localStorage = {};

        let _getItem = (key) => {

            let json = _localStorage[key];
            let value = json ? JSON.parse(json) : null;

            _localStorage[key] = json;

            try {
                json = $window.localStorage.getItem(key);
                json && (value = JSON.parse(json));
            }
            catch (error) { console.error(error); }

            return value;
        };
        let _setItem = (key, value) => {

            let json = JSON.stringify(value);

            _localStorage[key] = json;

            try { $window.localStorage.setItem(key, json); }
            catch (error) { console.error(error); }
        };
        let _removeItem = (key) => {

            delete _localStorage[key];

            try { $window.localStorage.removeItem(key); }
            catch (error) { console.error(error); }
        };

        let service = {};
        service.getItem = () => {

            return _getItem('auth');
        };
        service.setItem = (json) => {

            _setItem('auth', json);
        };
        service.removeItem = () => {

            _removeItem('auth');
        };

        return service;
    }]);