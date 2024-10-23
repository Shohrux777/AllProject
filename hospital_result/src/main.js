import '@fortawesome/fontawesome-free/css/all.min.css'
import 'bootstrap-css-only/css/bootstrap.min.css'
import 'mdbvue/lib/css/mdb.min.css'
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Vuelidate from 'vuelidate'
import i18n from './i18n'
import alert from "./components/alert.vue";
import Toast from "./components/alertNew.vue";
import AlertError from "./components/AlertError.vue";
import massage_box from "./components/massage_box.vue";
import loader from './components/loader.vue'

Vue.config.productionTip = false
import VueQRCodeComponent from 'vue-qrcode-component'
Vue.component('qr-code', VueQRCodeComponent)
Vue.component('Toast', Toast)
Vue.component('AlertError', AlertError)
Vue.component('alert', alert)
Vue.component('massage_box', massage_box)
Vue.component('loader', loader)
Vue.use(Vuelidate)

new Vue({
  router,
  store,
  i18n,
  render: h => h(App)
}).$mount('#app')
