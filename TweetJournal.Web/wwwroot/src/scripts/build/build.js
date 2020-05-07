const Bundler = require('parcel-bundler')
const path = require('path')

const entryFiles = [path.join(__dirname, '../../js/apps/JournalEntryApp.tsx')]
const options = {
  outDir: './js/dist',
  watch: process.env.NODE_ENV !== 'production',
  target: 'browser'
}

const build = async () => {
  const bundler = new Bundler(entryFiles, options)
  await bundler.bundle()
}

build()
