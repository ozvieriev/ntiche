angular.module('app.directives')
    .directive('ngDate', ['$filter', ($filter) => {

        return {
            restrict: 'A',
            scope: {
                date: '=ngDate',
                format: '=ngFormat'
            },
            link: (scope, element, attrs) => {

                if (!scope.date) return;

                element.addClass('text-muted small');

                var format = scope.format || 'yyyy-MM-dd HH:mm';
                scope.$watch('date', function (newValue, oldValue) {

                    if (newValue || newValue != oldValue)
                        element.html("<em>" + $filter('date')(scope.date, format) + "</em>");
                });
            }
        };
    }]);