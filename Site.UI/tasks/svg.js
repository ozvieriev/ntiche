const gulp = require('gulp');

gulp.task('svg', () => {

    return gulp.src('src/svg/**/*.*')
        .pipe(gulp.dest('ui/svg'));
});