const gulp = require('gulp');

gulp.task('json:i18n', () => {

    return gulp.src('src/i18n/**/*.json')
        .pipe(gulp.dest('ui/i18n'));
});
gulp.task('json:app:watch', () => {
    return gulp.watch('src/i18n/**/*.json', gulp.series('json:i18n'));
});
gulp.task('json', gulp.parallel('json:i18n'));