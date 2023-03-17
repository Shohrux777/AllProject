import { createStore } from 'vuex'
import department from './moduls/department'
import user from './moduls/user'
import dashboard from './moduls/dashboard'

export default createStore({
  state: {
    hostname: 'http://localhost:5003/api',
    hostname1: 'http://localhost:5003/',
  },
  getters: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    department,
    user,
    dashboard
  }
})
