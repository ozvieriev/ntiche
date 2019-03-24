const gulp = require('gulp');

gulp.task('images', () => {

    return gulp.src('src/images/**/*.*')
        .pipe(gulp.dest('ui/images'));
});