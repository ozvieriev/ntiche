angular.module('app')
    .config(['$translateProvider',
        ($translateProvider) => {

            // // $translateProvider.useLoader('$i18nLoader', {});
            // // $translateProvider.useLocalStorage();

            //$translateProvider.useLoader('$translatePartialLoader', {
            //    urlTemplate: 'i18n/{lang}-{part}.json'
            //});

            //$translateProvider.preferredLanguage('en');
            //$translateProvider.fallbackLanguage('en');

            //

            $translateProvider.registerAvailableLanguageKeys(['en', 'fr'], {
                'en': 'en',
                'fr': 'fr'
            });

            $translateProvider
                .useStaticFilesLoader({
                    prefix: 'i18n/',
                    suffix: `.json?version=${(window.currentVersionBuild || '')}`
                });

            //$translateProvider.preferredLanguage('en');
            //$translateProvider.use('en');
            ////$translateProvider.useCookieStorage();

            //$translateProvider.fallbackLanguage("en");
        }]);