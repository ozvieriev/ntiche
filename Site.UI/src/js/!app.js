angular.module('app.components', ['ui.router', 'pascalprecht.translate']);
angular.module('app.auth', []);
angular.module('app.services', []);
angular.module('app.controllers', []);
angular.module('app.directives', []);
angular.module('app.filters', []);

angular.module('app', ['app.components', 'app.auth', 'app.services', 'app.controllers', 'app.directives', 'app.filters'])
    .run(($transitions, $translate, $auth) => {

        if (location.hash.toLowerCase().startsWith('#!/fr/'))
            $translate.use('fr');

        //$trace.enable('TRANSITION');
        $transitions.onBefore({ to: '**' }, (transition) => {

            let to = transition.to();
            if (!to.roles ||

                $auth.isInRoles(to.roles))
                return;

            let stateService = transition.router.stateService;
            let locale = stateService.params.locale || 'en';

            let params = {
                returnUrl: to.url.replace(/^\//g, ''),
                locale: locale
            };

            return transition.router.stateService.target('account/sign-in', params);
        });

        //$transitions.onSuccess({ to: '**' }, (transition) => {

        //    let stateService = transition.router.stateService;
        //    let locale = stateService.params.locale;

        //    let currentLanguage = $translate.use();
        //    if (!currentLanguage || currentLanguage !== locale) {

        //        $translate.use(locale).then(() => {

        //            var d = $translate.use();
        //            $translate.refresh(d);
        //        });
        //    }
        //});
    });