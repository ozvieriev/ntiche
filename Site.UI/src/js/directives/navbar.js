angular.module('app.directives')
    .directive('ngNavbar', ['$window', ($window) => {

        return {
            restrict: 'A',

            link: (scope, element, attrs) => {

                element.find('a').on('click', () => {
                    angular.element('.navbar-collapse').collapse('hide');
                });
            }
        };
    }]);