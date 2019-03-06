angular.module('app.directives')
    .directive('ngNavbar', ['$window', ($window) => {

        return {
            restrict: 'A',

            link: (scope, element, attrs) => {
                
                element.find('li>a').on('click', function () {
                    angular.element('.navbar-collapse').collapse('hide');
                });
            }
        };
    }]);