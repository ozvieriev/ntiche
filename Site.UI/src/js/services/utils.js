angular.module('app.services')
    .factory('$utils', () => {

        let service = {};

        service.query = (query) => {

            let json = {};

            try {

                if (typeof query === 'undefined')
                    query = document.location.search;

                var split = query.replace(/(^\?)/, '').split('&');

                for (let i = 0; i < split.length; ++i) {

                    let item = split[i];
                    let index = item.indexOf('=');
                    if (index === -1)
                        continue;

                    let key = item.substring(0, index);
                    let value = item.substr(index + 1).trim();
                    json[key.toLowerCase()] = decodeURIComponent(value);
                }

            } catch (e) { }

            return json;
        };

        return service;
    });