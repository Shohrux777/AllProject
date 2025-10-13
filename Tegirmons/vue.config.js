const fs = require('fs')          // 👈 bu qatorni unutma
const path = require('path')
module.exports = {
  devServer: {  
    https: {
      key: fs.readFileSync(path.join(__dirname, 'tegirmon-key.pem')),
      cert: fs.readFileSync(path.join(__dirname, 'tegirmon.pem')),
    },
    historyApiFallback: {
      disableDotRule: true,
      rewrites: [
        { from: /^\/manifest\.json$/, to: '/manifest.json' }
      ]
    },
    disableHostCheck: true
  },
  pwa: {
    name: 'Tegirmon CRM',
    themeColor: '#171717',
    msTileColor: '#171717',
    manifestOptions: {
      start_url: '/',
      display: 'standalone',
    },
  },
  pluginOptions: {
    i18n: {
      locale: 'en',
      fallbackLocale: 'en',
      localeDir: 'locales',
      enableInSFC: false,
      includeLocales: false,
      enableBridge: true
    },
  }
}
