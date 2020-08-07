const { task, src, dest, watch } = require('gulp');

function css() {
  const postcss = require('gulp-postcss');
  const postcssPipe = postcss([
      require('precss'),
      require('tailwindcss'),
      require('autoprefixer')
  ])

  return src('./Styles/main.css')
      .pipe(postcssPipe)
      .pipe(dest('./wwwroot/css/'))
}

task('css', css)

task('css:watch', () => {
  watch(['./Styles/**/*.css, ./Styles/*.css'], function() {
    css()
  })
})