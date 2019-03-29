angular.module('app.directives')
    .directive('ngLoadingForm', ['$window', ($window) => {

        return {
            restrict: 'A',
            scope: {

            },
            link: (scope, element, attrs) => {

                let $submit = element.find('[type="submit"]:first');

                $submit
                    .addClass('btn btn-info');

                let $span = $submit.find('span:first');

                let watcher = scope.$watch('$parent.isBusy', (isBusy) => {

                    isBusy ?
                        element
                            .attr('disabled', 'disabled') :

                        element
                            .removeAttr('disabled');

                    isBusy ?
                        $submit
                            .addClass('btn-light'):
                        
                        $submit
                            .removeClass('btn-light');

                    isBusy ?
                        $span.removeClass('ng-hide') :
                        $span.addClass('ng-hide');
                });

                scope.$on('$destroy', watcher);
            }
        };
    }]);