import '@fortawesome/fontawesome-free/css/all.min.css'
import 'bootstrap-css-only/css/bootstrap.min.css'
import 'mdbvue/lib/css/mdb.min.css'
import Vue from 'vue'
import App from './App.vue'

// Vue.mixin({
//   methods: {
//     $numertoSumma(n) {
//       var tols = '';
//       var sum_str = '';
//       for(let i=0; i<n.length; i++){
//         if(n[i] != ' '){
//           tols += n[i];
//         }
//        }
//        if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
//         tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
//         tols = tols.slice(0,tols.length-1)
//        }
//        sum_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
//        var temp = ''
//        for(let i=0; i<sum_str.length; i++){
//         if(sum_str[i] != ' '){
//           temp += sum_str[i];
//         }
//        }
//       return parseFloat(temp);
//     }
//   }
// })


import router from './router'
import store from './store'
import i18n from './i18n'
import Vuelidate from 'vuelidate'
import Toast from "./components/alert.vue";
import Alert from "./components/toast.vue";
import massage_box from "./components/massage_box.vue";
import loader from './components/loader.vue'
import block from './components/block.vue'
import loaderFixed from './components/loaderFixed.vue'
import loaderTable from './components/loaderTable.vue'
import modal from './components/modal.vue'
import modal_new from './components/modal_new.vue'
import modal_right from './components/modal_right.vue'
import VueApexCharts from 'vue-apexcharts'
import VueQRCodeComponent from 'vue-qrcode-component'
import JsonExcel from "vue-json-excel";
import VueTheMask from 'vue-the-mask';
import ImageModal from "./components/ImageModal.vue";

Vue.component("ImageModal", ImageModal);

Vue.prototype.$imageModal = new Vue(ImageModal).$mount();
document.body.appendChild(Vue.prototype.$imageModal.$el);

Vue.use(VueTheMask)
Vue.component("downloadExcel", JsonExcel);
Vue.component('qr-code', VueQRCodeComponent)
Vue.component('apexchart', VueApexCharts)
Vue.config.productionTip = false
Vue.use(Vuelidate)
Vue.component('Toast', Toast)
Vue.component('Alert', Alert)
Vue.component('massage_box', massage_box)
Vue.component('modal-train', modal)
Vue.component('modal-right', modal_right)
Vue.component('modal-new', modal_new)
Vue.component('loader', loader)
Vue.component('block', block)
Vue.component('loaderTable', loaderTable)
Vue.component('loaderFixed', loaderFixed)

new Vue({
    router,
    store,
    i18n,
    render: h => h(App)
}).$mount('#app')