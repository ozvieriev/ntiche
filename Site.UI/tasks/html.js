const gulp = require('gulp');
const change = require('gulp-change');
const concat = require('gulp-concat');
const replace = require('gulp-replace');
const htmlmin = require('gulp-htmlmin');

const htmlminOptions = {
    collapseWhitespace: true,
    removeComments: true,
    sortAttributes: true,
    sortClassName: true
};

gulp.task('html:app', () => {

    return gulp.src(['src/index.html', 'src/ui/**/*.html'])
        .pipe(replace('nticheBuildVersion', Date.now()))
        .pipe(htmlmin(htmlminOptions))
        .pipe(change(function (content) {

            var relativePath = this.file.path.substr(this.file.base.length + 1);

            if (relativePath === 'index.html') {

                var version = Date.now();

                content = content.replace('.min.js', `.min.js?version=${version}`);
                content = content.replace('.min.css', `.min.css?version=${version}`);
                content = content.replace('CURRENT-VERSION-BUILD', version);
                
                return content;
            }
            relativePath = relativePath.replace(/\\/gi, '/');

            return `<script type="text/ng-template" id="${relativePath}">${content}</script>`;
        }))
        .pipe(concat('index.html'))
        .pipe(gulp.dest('ui'));
});
gulp.task('html:app:watch', () => {
    return gulp.watch('src/**/*.html', gulp.series('html:app'));
});
gulp.task('html', gulp.series('html:app'));