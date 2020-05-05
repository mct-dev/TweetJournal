const { compile } = require('./build.js')

compile({
  minify: false,
  sourcemap: true,
  define: {
    // double quotes required for esbuild
    // eslint-disable-next-line quotes
    'process.env.NODE_ENV': "'development'"
  }
})
