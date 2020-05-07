const { compile } = require('./build.js')

function buildDev () {
  compile({
    minify: true,
    sourcemap: true,
    define: {
      // double quotes required for esbuild
      // eslint-disable-next-line quotes
      'process.env.NODE_ENV': "'development'"
    }
  })
}

module.exports = buildDev
