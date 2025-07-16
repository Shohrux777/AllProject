import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

// // // import alert from './components/alert.vue'
// import axios from 'axios'

// axios.defaults.withCredentials = true // cookie yuborish uchun

// // Agar frontend Laravel blade ichida bo‘lsa:
// const token = document.querySelector('meta[name="csrf-token"]')?.getAttribute('content')
// if (token) {
//   axios.defaults.headers.common['X-CSRF-TOKEN'] = token
// }

import store from './store'
import 'mdb-vue-ui-kit/css/mdb.min.css';
import i18n from './i18n'
createApp(App).use(i18n).use(store).use(router).mount('#app')
