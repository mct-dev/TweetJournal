const { compile } = require('./build.js')

function buildProd () {
  compile({
    minify: true,
    sourcemap: false,
    define: {
      // double quotes required for esbuild
      // eslint-disable-next-line quotes
      'process.env.NODE_ENV': "'production'"
    }
  })
}

module.exports = buildProd
