
const gulp = require('gulp');
const browserSync = require('browser-sync').create();

gulp.task('serve', function () {

    browserSync.init({ server: { baseDir: '.' } });

    gulp
        .watch(['index.html', 'ui/css/**/*.css', 'ui/js/**/*.js'])
        .on('change', browserSync.reload);
});