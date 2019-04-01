angular.module('app')
    .config(($stateProvider, $urlRouterProvider) => {

        // $locationProvider.html5Mode({
        //     enabled: true,
        //     requireBase: false
        // });

        $urlRouterProvider.otherwise('/en/index');

        $stateProvider.state('app', {
            url: '/:locale',
            //templateUrl: 'index.html',
            restricted: false,
            abstract: true,
            views: {
                header: {
                    cache: false,
                    templateUrl: 'partial/header.html',
                    controller: 'partialHeaderController'
                },
                main: { controller: 'appController' },
                footer: {
                    cache: false,
                    templateUrl: 'partial/footer.html'
                }
            }
        });

        var _state = (json) => {

            json.name = json.name || json.url;
            json.htmlUrl = json.htmlUrl || json.url;
            json.params = json.params || {};
            json.templateUrl = json.templateUrl || `views/${json.htmlUrl}.html`;

            var state = {
                parent: 'app',
                url: `/${json.url}`,
                params: json.params,
                cache: false,
                templateUrl: json.templateUrl
            };

            if (json.controller)
                state.controller = `${json.controller}Controller`;

            if (json.roles)
                state.roles = json.roles;

            $stateProvider.state(json.name, state);
        };

        _state({ url: 'account', htmlUrl: 'account/index', controller: 'account', roles: [] });
        _state({ url: 'account/email-confirmation/:accountId/:emailConfirmationToken', htmlUrl: 'account/email-confirmation', controller: 'accountEmailConfirmation' });
        _state({ url: 'account/recover-password', controller: 'accountRecoverPassword' });
        _state({ url: 'account/reset-password/:accountId/:resetPasswordToken', htmlUrl: 'account/reset-password', controller: 'accountResetPassword' });
        _state({ url: 'account/sign-in', controller: 'accountSignIn', params: { returnUrl: null } });
        _state({ url: 'account/sign-up', controller: 'accountSignUp' });

        _state({ url: 'admin/report', controller: 'adminReport', roles: ['admin'] });

        _state({ url: 'exam/feedback', controller: 'examFeedback', roles: [], params: { examResultId: null } });
        _state({ url: 'exam/post-test-success', controller: 'examPostTestSuccess', roles: [], params: { examResultId: null } });
        _state({ url: 'exam/post-test', controller: 'examPostTest', roles: [] });
        _state({ url: 'exam', htmlUrl: 'exam/index', controller: 'exam', roles: [] });
        _state({ url: 'exam/pre-test', controller: 'examPreTest', roles: [] });

        _state({ url: 'webinar', htmlUrl: 'webinar/index', controller: 'webinar', roles: [] });

        _state({ url: 'about' });
        _state({ url: 'error' });
        _state({ url: 'index', controller: 'index' });
        _state({ url: 'privacy' });
        _state({ url: 'resources' });
        _state({ url: 'support' });
        _state({ url: 'terms' });
    });

//https://github.com/modularcode/modular-admin-angularjs/blob/master/src/_main.js