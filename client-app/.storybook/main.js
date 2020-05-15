'use strict'

const { join, resolve } = require('path')
const TsConfigPathsPlugin = require('tsconfig-paths-webpack-plugin')

const rootDir = resolve(__dirname, '..')

module.exports = {
  addons: [
    '@storybook/preset-create-react-app',
    '@storybook/addon-a11y',
    '@storybook/addon-actions',
    '@storybook/addon-backgrounds',
    '@storybook/addon-links',
    '@storybook/addon-knobs',
    '@storybook/addon-viewport',
  ],
  stories: ['../src/**/?(*.)stories.(md|ts)x'],
  webpackFinal: async (config) => {
    return {
      ...config,
      resolve: {
        ...config.resolve,
        extensions: ['.js', '.json', '.mjs', '.ts', '.tsx', '.wasm'],
        modules: ['node_modules', join(rootDir, 'src')],
        plugins: [new TsConfigPathsPlugin()],
      },
    }
  },
}
