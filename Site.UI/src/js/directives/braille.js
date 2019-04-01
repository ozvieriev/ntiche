angular.module('app.directives')
    .directive('ngBraille', ['$window', ($window) => {

        var template = [
            '<ul class="navbar-nav braille mb-3 mt-2">',
            '<li class="nav-item active" data-rem="1.0"><a class="nav-link">A</a></li>',
            '<li class="nav-item" data-rem="1.2"><a class="nav-link">A</a></li>',
            '<li class="nav-item" data-rem="1.5"><a class="nav-link">A</a></li>',
            '</ul>'
        ].join('');

        return {
            restrict: 'E',
            replace: true,
            template: template,

            link: (scope, element, attrs) => {

                var liCollection = element.find('li[data-rem]');

                liCollection.each((index, value) => {

                    var li = angular.element(value);
                    var rem = li.attr('data-rem');

                    li.css({
                        'font-size': `${rem}rem`,
                        'line-height': '1.7rem',
                        cursor: 'pointer'
                    });

                    li.find('a').bind('click', () => {

                        liCollection.removeClass('active');
                        liCollection.eq(index).addClass('active');

                        angular.element('html').css({
                            'font-size': `${rem}rem`
                        });
                    });
                });

                // element.find('a').bind('click', function () {

                //     var index = $(this).index();

                //     debugger
                //     $('body').css({ 'font-size': '1.2rem' });
                // });
            }
        };
    }]);