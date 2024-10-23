import Vue from 'vue'
import Vuex from 'vuex'
import laboratory from './modules/laboratory'

import position from './modules/program/position'
import rooms from './modules/program/rooms'
import users from './modules/program/users'
import contragent from './modules/program/kontragent'
import province from './modules/program/province'
import district from './modules/program/district'
import client from './modules/program/client'
import send_doc from './modules/program/send_doc'
import servicetype from './modules/program/servicetype'
import patient_type from './modules/program/patient_type'

import bron from './modules/bron'
import authorization from './modules/hospital/authorization'
import cashbox from './modules/hospital/cashbox'
import doctor from './modules/hospital/doctor'
import report from './modules/hospital/report'
import servGroup from './modules/hospital/servGroup'
import dragMin from './modules/hospital/dragmin'
import serviceGroupShowDoc from './modules/hospital/serviceGroupShowDoc'
import activeService from './modules/hospital/activeService'
import laboratory_up from './modules/hospital/laboratory_up'
import vozvrat from './modules/hospital/vozvrat'



Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    // hostname: 'http://192.168.1.8:5003/api',
    // hostname1: 'http://192.168.1.8:5003/',
    // hostname2: 'http://192.168.1.8:8088/',
    hostname: 'http://89.104.102.23:5003/api',
    hostname1: 'http://89.104.102.23:5003/',
    hostname2: 'http://89.104.102.23:8088/',


    alert: false,
        info_product: false,
        cashPay_card: true,
        patient_name_for_cash: '',
        patient_born_date_for_cash: '',
        patent_address_for_check: '',
        patent_cantragent_for_check: '',
        get_analiz_list: [],
        
        result_list: [],
        shablonLists: [],
        debitResultPatient: [{
        real_qty:0
        }],

          
        shablon_content: {
            name: '',
            title: ''
        },
        sablonSavepatient: {
            content: '',
            title: ''
        },
        patientIdforDoctor: null,
        pagination: {
            page: 0,
            size: 10
        },
        current_list: {
            current_item_count: 10,
            current_page: 1,
            items_count: localStorage.Items_count,
        },
        ochred: {
            doc_name : '',
            client_name: '',
            born_date: '',
            phone_number: ''
        },
        ochredService: [],
        dontShowPagination: true,
  },
  getters: {
    get_pagination(state) {
        return state.pagination;
    },
    get_current_list(state) {
        return state.current_list
    },
    dontShowPagination(state) {
        return state.dontShowPagination
    },
    result_list(state) {
        return state.result_list
    },
    shablonLists(state) {
        return state.shablonLists
    },
    get_analiz_list(state) {
        return state.get_analiz_list
    },
    debitResultPatient(state) {
        return state.debitResultPatient
    },
    shablon_content(state) {
        return state.shablon_content
    },
    sablonSavepatient(state) {
        return state.sablonSavepatient
    },
    patientIdforDoctor(state) {
        return state.patientIdforDoctor
    },
    get_ochred(state){
        return state.ochred
    },
    get_ochredService(state){
        return state.ochredService
    }
    
  },  
  mutations: {
    updatePatientId(state, data){
        state.patientIdforDoctor = data
    },
    updatePatentResults(state, data){
        state.result_list = data
    },
    UpdateshablonLists(state, data){
        // console.log('men uchun ham')
        // console.log(data)
        state.shablonLists = data
    },
    UpdateAnalizList(state,data){
        // console.log('analiz list')
        // console.log(data)
        state.get_analiz_list = data
    },
    Updatedebit(state, data){
        state.debitResultPatient[0].real_qty = data
    },
    updateShablon(state, data){
        state.shablon_content.name = data.name
        state.shablon_content.title = data.title
    },
    updateShablonPatient(state, data){
        state.sablonSavepatient.content = data.content
        state.sablonSavepatient.title = data.title
    },
    update_patient_name(state, data) {
        state.patient_name_for_cash = data.name;
        state.patient_born_date_for_cash = data.born;
        state.patent_address_for_check = data.address;
        state.patent_cantragent_for_check = data.contragent;
    },
    update_pagination(state, { size, page }) {
        // console.log('hiy girl')
        // console.log(size)
        // console.log(page)
        state.pagination.page = page;
        state.pagination.size = size;
        //   console.log(size)
    },
    update_pagination_first(state, { current_item_count, current_page, items_count }) {
        state.current_list.current_item_count = current_item_count;
        state.current_list.current_page = current_page;
        state.current_list.items_count = items_count;
        localStorage.Items_count = items_count
        state.dontShowPagination = true
    },
    ochred_add_check(state, data){
        state.ochred =  data
    },
    ochred_add_service(state, data){
        state.ochredService =  data
    }
},
  actions: {
  },
  modules: {
     laboratory,
     position,
     rooms,
     users,
     contragent,
     province,
     district,
     client,
     send_doc,
     servicetype,
     patient_type,
     bron,

     authorization,
     cashbox,
     doctor,
     report,
     servGroup,
     dragMin,
     serviceGroupShowDoc,
     activeService,
     laboratory_up,
     vozvrat
  }
})
