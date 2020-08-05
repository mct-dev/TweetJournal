const { task, src, dest } = require('gulp');

task('css', () => {
    const postcss = require('gulp-postcss');
    const postcssPipe = postcss([
        require('precss'),
        require('tailwindcss'),
        require('autoprefixer')
    ])

    return src('./Styles/main.css')
        .pipe(postcssPipe)
        .pipe(dest('./wwwroot/css/'))
})