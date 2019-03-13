angular.module('app.directives')
    .directive('ngLoadingForm', ['$window', ($window) => {

        return {
            restrict: 'A',
            scope: {

            },
            link: (scope, element, attrs) => {

                let $submit = element.find('[type="submit"]:first');


                $submit
                    .addClass('btn btn-primary')
                    .prepend('<span class="spinner-border spinner-border-sm mr-2 ng-hide"></span>');

                let $span = $submit.find('span:first');

                let watcher = scope.$watch('$parent.isBusy', (isBusy) => {


                    isBusy ?
                        element
                            .attr('disabled', 'disabled') :

                        element
                            .removeAttr('disabled');

                    isBusy ?
                        $submit
                            .addClass('btn-light')
                            .attr('disabled', 'disabled'):
                        
                        $submit
                            .removeClass('btn-light')
                            .removeAttr('disabled');

                    isBusy ?
                        $span.removeClass('ng-hide') :
                        $span.addClass('ng-hide');
                });

                scope.$on('$destroy', watcher);
            }
        };
    }]);