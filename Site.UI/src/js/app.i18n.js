angular.module('app')
    .config(['$translateProvider', '$translatePartialLoaderProvider',
        ($translateProvider, $translatePartialLoaderProvider) => {

            // // $translateProvider.useLoader('$i18nLoader', {});
            // // $translateProvider.useLocalStorage();

            $translateProvider.useLoader('$translatePartialLoader', {
                urlTemplate: 'i18n/{part}/{lang}.json'
            });

            $translateProvider.preferredLanguage('en');
            $translatePartialLoaderProvider.addPart('global/button');
        }]);