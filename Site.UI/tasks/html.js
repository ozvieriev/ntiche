const gulp = require('gulp');
const change = require('gulp-change');
const concat = require('gulp-concat');
const htmlmin = require('gulp-htmlmin');

const htmlminOptions = {
    collapseWhitespace: true,
    removeComments: true,
    sortAttributes: true,
    sortClassName: true
};

gulp.task('html:app', () => {

    return gulp.src(['src/index.html', 'src/ui/**/*.html'])
        .pipe(htmlmin(htmlminOptions))
        // .pipe(change(function (content) {

        //     var relativePath = this.file.path.substr(this.file.base.length + 1);

        //     if (relativePath === 'index.html')
        //         return content;

        //     relativePath = relativePath.replace(/\\/gi, '/');

        //     return `<script type="text/ng-template" id="${relativePath}">${content}</script>`;
        // }))
        // .pipe(concat('index.html'))
        .pipe(gulp.dest('ui'));
});
gulp.task('html:app:watch', () => {
    return gulp.watch('src/**/*.html', gulp.series('html:app'));
});
gulp.task('html', gulp.series('html:app'));