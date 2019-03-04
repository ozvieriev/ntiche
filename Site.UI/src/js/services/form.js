angular.module('app.services')
    .factory('$form', () => {

        var service = {};

        service.submit = (entity, form, callback) => {

            if (form.$valid !== true) {

                angular.forEach(form, (value, key) => {

                    if (typeof value === 'object' && value.hasOwnProperty('$modelValue'))
                        value.$setDirty();
                });
            }
            if (service.isReady(entity, form) === false)
                return;
            
            callback(form);
        };
        service.isReady = (entity, form) => {

            if (entity.isBusy === true || form.$valid !== true)
                return false;

            entity.isBusy = true;

            return true;
        };

        return service;
    });