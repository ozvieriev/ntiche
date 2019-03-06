const gulp = require('gulp');

gulp.task('watch', gulp.parallel(
    'css:app:watch',
    'html:app:watch',
    'js:app:watch',
    'json:app:watch'));