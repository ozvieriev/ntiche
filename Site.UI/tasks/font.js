const gulp = require('gulp');

gulp.task('font:font-awesome-5', () => {
    return gulp.src([
        'node_modules/font-awesome-5-css/webfonts/**/*.*'
    ])
        .pipe(gulp.dest('ui/webfonts'));
});
gulp.task('font', gulp.series(gulp.parallel('font:font-awesome-5')));