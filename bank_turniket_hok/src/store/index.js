import { createStore } from 'vuex'
import department from './moduls/department'
import user from './moduls/user'
import dashboard from './moduls/dashboard'
import company from './moduls/company'
import smena from './moduls/smena'
import door from './moduls/door'
import device from './moduls/device'
import salary from './moduls/salary'
import add_rasxod_note from './moduls/add_rasxod_note'

export default createStore({
  state: {
    // hostname: 'http://192.168.11.2:5003/api',
    // hostname1: 'http://192.168.11.2:5003/',

    
    hostname: 'http://localhost:5003/api',
    hostname1: 'http://localhost:5003/',
    // hostname: 'http://89.104.102.23:5003/api',
    // hostname1: 'http://89.104.102.23:5003/',


    // hostname: 'http://192.168.100.222:5003/api',
    // hostname1: 'http://192.168.100.222:5003/',
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
    salary,
    add_rasxod_note
  }
})
