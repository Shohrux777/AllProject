module.exports = {
  devServer: {  
    https: true,
    disableHostCheck: true
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
