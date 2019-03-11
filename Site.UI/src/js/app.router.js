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
                    templateUrl: 'partial/header.html',
                    controller: 'partialHeaderController'
                },
                main: { controller: 'appController', },
                footer: { templateUrl: 'partial/footer.html' }
            }
        });

        var _state = (json) => {

            json.name = json.name || json.url;
            json.htmlUrl = json.htmlUrl || json.url;
            json.params = json.params || {};
            json.templateUrl = json.templateUrl || `views/${json.htmlUrl}.html`;
            json.isProtected = !!json.isProtected;

            var state = {
                parent: 'app',
                url: `/${json.url}`,
                params: json.params,
                templateUrl: json.templateUrl,
                isProtected: json.isProtected
            };

            if (json.controller)
                state.controller = `${json.controller}Controller`;

            $stateProvider.state(json.name, state);
        };

        _state({ url: 'account', htmlUrl: 'account/index', controller: 'accountIndex', isProtected: true });
        _state({ url: 'account/email-confirmation/:accountId/:emailConfirmationToken', htmlUrl: 'account/email-confirmation', controller: 'accountEmailConfirmation' });
        _state({ url: 'account/recover-password', controller: 'accountRecoverPassword' });
        _state({ url: 'account/reset-password/:accountId/:resetPasswordToken', htmlUrl: 'account/reset-password', controller: 'accountResetPassword' });
        _state({ url: 'account/sign-in', controller: 'accountSignIn', params: { returnUrl: null } });
        _state({ url: 'account/sign-up', controller: 'accountSignUp' });

        _state({ url: 'exam/feedback', controller: 'examFeedback', isProtected: true });
        _state({ url: 'exam/post-test', controller: 'examPostTest', isProtected: true });
        _state({ url: 'exam/pre-test', controller: 'examPreTest', isProtected: true });

        _state({ url: 'about' });
        _state({ url: 'contact-us' });
        _state({ url: 'disclaimer' });
        _state({ url: 'error' });
        _state({ url: 'index', controller: 'index' });
        _state({ url: 'privacy' });
        _state({ url: 'resources' });
    });

//https://github.com/modularcode/modular-admin-angularjs/blob/master/src/_main.js