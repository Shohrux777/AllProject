import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'mdb-vue-ui-kit/css/mdb.min.css';
import i18n from './i18n'

createApp(App).use(i18n).use(store).use(router).mount('#app')
