angular.module('app.directives')
    .directive('ngLoadingForm', ['$window', ($window) => {

        return {
            restrict: 'A',
            scope: {

            },
            link: (scope, element, attrs) => {

                let $submit = element.find('[type="submit"]');

                $submit.addClass('btn btn-primary');

                let watcher = scope.$watch('$parent.isBusy', (isBusy) => {

                    isBusy ?
                        $submit.addClass('btn-light') :
                        $submit.removeClass('btn-light');
                });

                scope.$on('$destroy', watcher);
            }
        };
    }]);