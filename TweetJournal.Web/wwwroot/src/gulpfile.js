'use strict'

const { watch, series, src, dest } = require('gulp')
const sass = require('gulp-sass')

function watchSass (cb) {
  watch(['css/**/*.{scss,sass}', '!css/dist/**/*'], buildSass)
  cb()
}

function buildSass (cb) {
  src('css/**/*.{scss,sass}')
    .pipe(sass().on('error', sass.logError))
    .pipe(dest('css/dist'))
  cb()
}

exports.watch = series(buildSass, watchSass)
exports.default = buildSass
