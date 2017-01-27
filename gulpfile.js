var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('compile-scss', function(){
    gulp.src('wwwroot/sass/**/*.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('wwwroot/css/'));
})

//Watch task
gulp.task('default', function() {
    gulp.watch('wwwroot/sass/**/*.scss', ['compile-scss']);
})