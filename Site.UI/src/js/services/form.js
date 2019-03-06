angular.module('app.services')
    .factory('$form', () => {

        var service = {};

        service.submit = ($scope, form, callback) => {

            if (form.$valid !== true) {

                angular.forEach(form, (value, key) => {

                    if (typeof value === 'object' && value.hasOwnProperty('$modelValue'))
                        value.$setDirty();
                });
            }
            if (service.isReady($scope, form) === false)
                return;

            $scope.status = null;
            $scope.error = null;

            callback(form);
        };
        service.isReady = ($scope, form) => {

            if ($scope.isBusy === true || form.$valid !== true)
                return false;

            $scope.isBusy = true;

            return true;
        };

        return service;
    });