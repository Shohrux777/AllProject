import { createStore } from 'vuex'
import department from './moduls/department'
import user from './moduls/user'
import dashboard from './moduls/dashboard'
import company from './moduls/company'
import smena from './moduls/smena'
import door from './moduls/door'
import device from './moduls/device'
import salary from './moduls/salary'

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
    dashboard,
    company,
    door,
    device,
    smena,
    salary
  }
})
