const gulp = require('gulp');
const concat = require('gulp-concat');
const cleanCSS = require('gulp-clean-css');

gulp.task('css:app', () => {
    
    return gulp.src([
        'src/css/app/**/*.css'
    ])
        .pipe(concat('app.min.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('ui/css'));
});
gulp.task('css:vendor', () => {
    
    return gulp.src([
        'node_modules/bootstrap/dist/css/bootstrap.min.css',
        'node_modules/font-awesome-5-css/css/all.min.css'/*,
        'node_modules/balloon-css/balloon.min.css',*/
    ])
        .pipe(concat('vendor.min.css'))
        .pipe(gulp.dest('ui/css'));
});
gulp.task('css:app:watch', () => {
    return gulp.watch('src/css/**/*.css', gulp.series('css:app'));
});
gulp.task('css', gulp.series(gulp.parallel('css:app', 'css:vendor')));