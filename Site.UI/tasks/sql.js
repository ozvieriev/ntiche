const gulp = require('gulp');
const concat = require('gulp-concat');

gulp.task('sql:app', () => {
    
    return gulp.src([
        'sql/schema.sql',
        'sql/table.sql',
        'sql/sp.sql'
    ])
        .pipe(concat('db.sql'))
        .pipe(gulp.dest('./'));
});
gulp.task('sql:app:watch', () => {
    return gulp.watch('sql/**/*.sql', gulp.series('sql:app'));
});
gulp.task('sql', gulp.series('sql:app'));