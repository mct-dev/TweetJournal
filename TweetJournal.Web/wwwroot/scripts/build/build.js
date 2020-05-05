const { build } = require('esbuild')

function compile (optionsOverrides) {
  const options = {
    stdio: 'inherit',
    platform: 'browser',
    entryPoints: ['./app/app.tsx'],
    outfile: './public/app.js',
    minify: true,
    bundle: true,
    sourcemap: false,
    ...optionsOverrides
  }

  build(options).catch((err) => {
    console.log(err)
    process.exit(1)
  })
}

module.exports = { compile }
