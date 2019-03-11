
angular.module('app.components', ['ui.router', 'pascalprecht.translate']);
angular.module('app.auth', []);
angular.module('app.services', []);
angular.module('app.controllers', []);
angular.module('app.directives', []);
angular.module('app.filters', []);

angular.module('app', ['app.components', 'app.auth', 'app.services', 'app.controllers', 'app.directives', 'app.filters'])
    .run(($trace, $transitions, $auth, $translate, $translatePartialLoader) => {

        $trace.enable('TRANSITION');
        $transitions.onBefore({ to: '**' }, (transition) => {

            var to = transition.to();
            var stateService = transition.router.stateService;
            var locale = stateService.params.locale || 'en';

            if (!to.roles || 
                
                $auth.isInRoles(to.roles))
                return;

            var params = {
                returnUrl: to.url.replace(/^\//g, ''),
                locale: locale
            };

            return transition.router.stateService.target('account/sign-in', params);
        });

        $transitions.onSuccess({ to: '**' }, (transition) => {

            let stateService = transition.router.stateService;
            let locale = stateService.params.locale || 'en';

            $translate.use(locale);
        });
    });