const { src, dest, series, parallel, watch } = require('gulp')
const less = require('gulp-less')
const minifyCSS = require('gulp-csso')

function watchCss() {
  return watch(['css/**/*.less'], css)
}

function css() {
  return src('css/bci.less')
    .pipe(less())
    .pipe(minifyCSS())
    .pipe(dest('build/css'))
}

exports.watchCss = series(css, watchCss)
exports.css = css
exports.default = parallel(css)