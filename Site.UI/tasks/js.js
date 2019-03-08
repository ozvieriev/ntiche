const fn = require('gulp-fn');
const del = require('del');
const gulp = require('gulp');
const path = require('path');
const babel = require('gulp-babel');
const minify = require('gulp-minify');
const concat = require('gulp-concat');

gulp.task('js:app', () => {

    return gulp.src('src/js/**/*.js')
        .pipe(concat('app.min.js'))
        .pipe(babel({
            presets: ['@babel/env']
        }))
        // .pipe(minify({
        //     ext: {
        //         src: '.js',
        //         min: '.min.js'
        //     },
        //     mangle: false,
        //     ignoreFiles: ['.min.js']
        // }))
        .pipe(gulp.dest('ui/js'))
        .pipe(fn(function (file) {

            if (path.basename(file.path) === 'app.js')
                del(file.path);
        }))
        .on('end', () => {

            return gulp.src([
                'ui/js/vendor.min.js',
                'ui/js/app.min.js'
            ])
                .pipe(concat('bundle.min.js'))
                .pipe(gulp.dest('ui/js'));
        });
});
gulp.task('js:vendor', () => {

    return gulp.src([
        'node_modules/jquery/dist/jquery.slim.min.js',
        'node_modules/bootstrap/dist/js/bootstrap.min.js',
        'node_modules/angular/angular.min.js',
        'node_modules/angular-ui-router/release/angular-ui-router.min.js',
        'node_modules/angular-translate/dist/angular-translate.min.js',
        'node_modules/angular-translate/dist/angular-translate-loader-partial/angular-translate-loader-partial.min.js'
    ])
        .pipe(concat('vendor.min.js'))
        .pipe(gulp.dest('ui/js'));
});
gulp.task('js:app:watch', () => {
    return gulp.watch('src/js/**/*.js', gulp.series('js:app'));
});
gulp.task('js', gulp.series('js:vendor', 'js:app'));