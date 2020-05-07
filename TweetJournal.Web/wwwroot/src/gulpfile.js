'use strict'

const { watch, series, parallel, src, dest } = require('gulp')
const sass = require('gulp-sass')
const buildDev = require('./scripts/build/build-dev.js')
const buildProd = require('./scripts/build/build-prod.js')
const isProduction = process.env.NODE_ENV === 'production'

function watchJs (cb) {
  watch(['js/**/*.{js,jsx,ts,tsx}', '!js/dist/**/*'], buildJs)
  cb()
}

function buildJs (cb) {
  if (isProduction) {
    console.log('Compiling js for production...')
    buildProd()
    cb()
    return
  }

  console.log('Compiling js for development...')
  buildDev()
  cb()
}

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

exports.watch = parallel(series(buildJs, watchJs), series(buildSass, watchSass))
exports.default = buildJs
