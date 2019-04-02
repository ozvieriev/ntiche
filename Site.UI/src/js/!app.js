angular.module('app.components', ['ui.router', '720kb.datepicker', 'pascalprecht.translate']);
angular.module('app.auth', []);
angular.module('app.services', []);
angular.module('app.controllers', []);
angular.module('app.directives', []);
angular.module('app.filters', []);

angular.module('app', ['app.components', 'app.auth', 'app.services', 'app.controllers', 'app.directives', 'app.filters'])
    .run(($transitions, $translate, $templateCache, $auth) => {

        let tempaltes = document.querySelectorAll('script[type="text/ng-template"]');
        angular.forEach(tempaltes, (template) => {
            $templateCache.put(template.id, template.innerHTML);
        });

        let locationHash = location.hash.toLowerCase();

        if (locationHash.indexOf('#!/fr/') === 0)
            $translate.use('fr'); else
            $translate.use('en');

        //$trace.enable('TRANSITION');
        $transitions.onBefore({ to: '**' }, (transition) => {

            let to = transition.to();
            if (!to.roles || $auth.isInRoles(to.roles))
                return;

            let stateService = transition.router.stateService;
            let locale = stateService.params.locale || 'en';

            let params = {
                returnUrl: to.url.replace(/^\//g, ''),
                locale: locale
            };

            return transition.router.stateService.target('account/sign-in', params);
        });
    });