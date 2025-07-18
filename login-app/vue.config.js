const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true
});

// vue.config.js
const path = require('path');

module.exports = {
  configureWebpack: {
    resolve: {
      alias: {
        '@poc': path.resolve(__dirname, 'src/modules/poc-api-tester/src')
      }
    }
  }
}