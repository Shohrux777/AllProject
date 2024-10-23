import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [{
        path: '/login',
        name: 'login',
        meta: { layout: 'login' },
        component: () =>
            import ('../views/Login.vue')
    },
    {
        path: '/',
        name: 'loginArchieve',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/archieve/login.vue')
    },
    {
        path: '/dashboard',
        name: 'dashboard',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/dashboard.vue')
    },
    {
        path: '/position',
        name: 'position',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/position.vue')
    },
    {
        path: '/profil',
        name: 'profil',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/any_prog/profil.vue')
    },
    {
        path: '/rooms',
        name: 'rooms',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/rooms.vue')
    },
    {
        path: '/depart',
        name: 'depart',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/department.vue')
    },
    {
        path: '/service_type',
        name: 'service_type',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/servicetype.vue')
    },
    {
        path: '/number_into',
        name: 'number_into',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/number_into.vue')
    },
    {
        path: '/patient_type',
        name: 'patient_type',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/patient_type.vue')
    },
    {
        path: '/contragent',
        name: 'contragent',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/kontragent.vue')
    },
    {
        path: '/province',
        name: 'province',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/province.vue')
    },
    {
        path: '/district',
        name: 'district',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/district.vue')
    },
    {
        path: '/company',
        name: 'company',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/company.vue')
    },
    {
        path: '/client',
        name: 'client',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/client.vue')
    },
    {
        path: '/medicine',
        name: 'medicine',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/medicineList.vue')
    },
    {
        path: '/labGroup',
        name: 'labGroup',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/any_prog/labGroup.vue')
    },
    
    {
        path: '/medicineMin',
        name: 'medicineMin',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/medicineMin.vue')
    },
    {
        path: '/agent_add/:id',
        name: 'agent_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/agent/agent_add.vue')
    },
    {
        path: '/agent',
        name: 'agent',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/agent/agent.vue')
    },
    {
        path: '/send_doc',
        name: 'send_doc',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/send_doctor.vue')
    },
    {
        path: '/cashbox',
        name: 'cashbox',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/cashbox.vue')
    },
    {
        path: '/cashbox1',
        name: 'cashbox1',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/cashbox1.vue')
    },
    {
        path: '/result_lab',
        name: 'result_lab',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/result_lab.vue')
    },
    {
        path: '/result_lab/:id',
        name: 'result_labID',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/result_labPrint.vue')
    },
    {
        path: '/result_xulosa/:id',
        name: 'result_xulosaID',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/result_xulosa.vue')
    },
    {
        path: '/doctorShowlab/:id',
        name: 'doctorShowlabID',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/doctorShowlab.vue')
    },
    {
        path: '/doctorShow_shablon/:id',
        name: 'doctorShow_shablonID',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/doctorShow_shablon.vue')
    },
    {
        path: '/pay_debit',
        name: 'pay_debit',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/pay_debit.vue')
    },
    {
        path: '/doctor_info',
        name: 'doctor_info',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/send doctor/doctor_info.vue')
    },
    {
        path: '/debit_doc',
        name: 'debit_doc',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/send doctor/debit_doc.vue')
    },
    {
        path: '/doc_info_drag',
        name: 'doc_info_drag',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/doc_func/doc_drug_info.vue')
    },
    {
        path: '/doc_patient_info',
        name: 'doc_patient_info',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/doc_func/doc_patient_info.vue')
    },
    {
        path: '/payment_docReport',
        name: 'payment_docReport',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/payment_docReport.vue')
    },
    {
        path: '/pay_salary_list',
        name: 'pay_salary_list',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/pay_salary_list.vue')
    },
    {
        path: '/pay_salary_list_all',
        name: 'pay_salary_list_all',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/pay_salary_list_all.vue')
    },
    {
        path: '/bron_docReport',
        name: 'bron_docReport',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bron_docReport.vue')
    },
    {
        path: '/update',
        name: 'update',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/update.vue')
    },
    {
        path: '/check',
        name: 'check',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/hospital/check.vue')
    },
    {
        path: '/checkDebit',
        name: 'checkDebit',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/hospital/checkDebit.vue')
    },
    {
        path: '/bron',
        name: 'bron',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/bron/bron.vue')
    },
    {
        path: '/not_payed_list_regis',
        name: 'not_payed_list_regis',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/bron/not_payed_list_regis.vue')
    },
    {
        path: '/invoice_add/:id',
        name: 'invoice_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/invoice/invoice_add.vue')
    },
    {
        path: '/package_add/:id',
        name: 'package_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/package_service/package_add.vue')
    },
    {
        path: '/package',
        name: 'package',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/package_service/package.vue')
    },
    {
        path: '/word',
        name: 'word',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/word.vue')
    },
    {
        path: '/selectShablon',
        name: 'selectShablon',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/selectShablon.vue')
    },
    {
        path: '/blankaPrint',
        name: 'blankaPrint',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/blankaPrint.vue')
    },
    {
        path: '/blankaPrint/:id',
        name: 'blankaPrinted',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/blankaPrinted.vue')
    },
    {
        path: '/info',
        name: 'info',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/info.vue')
    },
    {
        path: '/word_search',
        name: 'word_search',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/Editor/word_search.vue')
    },
    {
        path: '/editor_doc',
        name: 'editor_doc',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/editor/editor_doc.vue')
    },
    {
        path: '/bycontragent',
        name: 'bycontragent',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bycontragent.vue')
    },
    {
        path: '/bydoctorMoneyEarn',
        name: 'bydoctorMoneyEarn',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bydoctorMoneyEarn.vue')
    },
    {
        path: '/bytime',
        name: 'bytime',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bytime.vue')
    },
    {
        path: '/mazok',
        name: 'mazokpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok.vue')
    },
    {
        path: '/mazok_add/:id',
        name: 'mazok',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok_add.vue')
    },
    {
        path: '/mazok_show/:id',
        name: 'mazok_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/mazok_print.vue')
    },
    {
        path: '/mazok_print/:id',
        name: 'mazok_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/mazok_print.vue')
    },
    {
        path: '/bytime_kassir',
        name: 'bytime_kassir',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/kassir_report/bytime.vue')
    },
    {
        path: '/covidExpress_show/:id',
        name: 'covidExpress_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/covidExpress_print.vue')
    },
    {
        path: '/covidExpress_print/:id',
        name: 'covidExpress_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/covidExpress_print.vue')
    },
    {
        path: '/dailyForPayment_kassir',
        name: 'dailyForPayment_kassir',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/kassir_report/dailyForPayment.vue')
    },
    {
        path: '/bytimeservice_bron',
        name: 'bytimeservice_bron',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bytimeservice_bron.vue')
    },
    {
        path: '/infoPatient',
        name: 'infoPatient',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/infoPatient.vue')
    },
    {
        path: '/debitReport',
        name: 'debitReport',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/debitReport.vue')
    },
    {
        path: '/debitpayReport',
        name: 'debitpayReport',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/debitpayReport.vue')
    },
    {
        path: '/debitDetiels/:id',
        name: 'debitDetiels3',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/debitDetiels.vue')
    },
    {
        path: '/bydate',
        name: 'bydate',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bydate.vue')
    },
    {
        path: '/bonus_contragent',
        name: 'bonus_contragent',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/bodus_add/addBonus.vue')
    },
    {
        path: '/bonus_full',
        name: 'bonus_full',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/bodus_add/fullInfo.vue')
    },
    {
        path: '/bonus_repot',
        name: 'bonus_repot',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/bodus_add/contragent_report.vue')
    },
    {
        path: '/bykassir',
        name: 'bykassir',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/kassa_date.vue')
    },
    {
        path: '/kunlik',
        name: 'kunlik',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/kunlik.vue')
    },
    {
        path: '/kunlikkassa',
        name: 'kunlikkassa',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/kunlikkassa.vue')
    },
    {
        path: '/daily',
        name: 'daily',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/daily.vue')
    },
    {
        path: '/dailyForPayment',
        name: 'dailyForPayment',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/dailyForPayment.vue')
    },
    {
        path: '/groupReport',
        name: 'groupReport',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/groupReport.vue')
    },
    {
        path: '/apply_admin',
        name: 'apply_admin',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/adminReturnMoneyList.vue')
    },
    {
        path: '/apply_kassir',
        name: 'apply_kassir',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/kassirReturnMoney.vue')
    },
    {
        path: '/check_payment_list',
        name: 'check_payment_list',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/check_payment_list.vue')
    },
    {
        path: '/return_report',
        name: 'return_report',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/returnedReport.vue')
    },
    {
        path: '/return_money',
        name: 'return_money',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/bodus_add/return_money.vue')
    },
    {
        path: '/bycont_data',
        name: 'bycont_data',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/contragentService.vue')
    },
    {
        path: '/ServiceGroup_show_doc',
        name: 'ServiceGroup_show_doc',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/ServiceGroup_show_doc.vue')
    },
    {
        path: '/activeServiceRemove',
        name: 'activeServiceRemove',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/hospital/activeServiceRemove.vue')
    },
    {
        path: '/byuzi_date',
        name: 'byuzi_date',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/contUziDate.vue')
    },
    {
        path: '/doctor',
        name: 'doctor',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/doctor_page.vue')
    },
    {
        path: '/laboratory_list/:id',
        name: 'laboratory_list',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/dynamic/labTable_andAdd.vue')
    },


    {
        path: '/m_users',
        name: 'm_user',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/users.vue')
    },
    
    {
        path: '/Laboratoriya_add',
        name: 'Laboratoriya_add',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/Laboratoriya_add')
    },
    {
        path: '/servecResultDynamic',
        name: 'servecResultDynamic',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/servecResultDynamic')
    },
    {
        path: '/paymentsGroupLab',
        name: 'paymentsGroupLab',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/paymentsGroupLab')
    },
    {
        path: '/lab_print/:id',
        name: 'lab_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/lab_print.vue')
    },
    {
        path: '/lab_print_id/:id',
        name: 'lab_print_id',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/lab_print_id.vue')
    },
    {
        path: '/lab_show/:id',
        name: 'lab_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/lab_show.vue')
    },
    {
        path: '/labUpdate/:id',
        name: 'labUpdate',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/dynamic/labUpdate.vue')
    },
    {
        path: '/analysis_list',
        name: 'analysis_list',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/analysis/dynamic/analysis_list.vue')
    },




    
    {
        path: '/covidExpress',
        name: 'covidExpresspage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/covidExpress.vue')
    },
    {
        path: '/covidExpress_add/:id',
        name: 'covidExpress',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/covidExpress_add.vue')
    },
    {
        path: '/covidQon',
        name: 'covidQonpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/covidQon.vue')
    },
    {
        path: '/covidQon_add/:id',
        name: 'covidQon',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/covidQon_add.vue')
    },
    




    {
        path: '/bronRoom',
        name: 'bronRoom',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/bron/bronRoom.vue')
    },
    {
        path: '/bronBeds',
        name: 'bronBeds',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/bron/bronBeds.vue')
    },
    {
        path: '/bronRoomAddBeds',
        name: 'bronRoomAddBeds',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/bron/bronRoomAddBeds.vue')
    },
    {
        path: '/priceTypeRoom',
        name: 'priceTypeRoom',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/bron/priceTypeRoom.vue')
    },
    {
        path: '/not_payed_list',
        name: 'not_payed_list',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/bron/not_payed_list.vue')
    },
    {
        path: '/doc_patient_list',
        name: 'doc_patient_list',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/bron/doc_patient_list.vue')
    },
    {
        path: '/bron_time_payment',
        name: 'bron_time_payment',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/bron_time_payment.vue')
    },
    {
        path: '/report_of_reagin',
        name: 'report_of_reagin',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/hospital/report/report_of_reagin.vue')
    },
    {
        path: '/otcheritList/:id',
        name: 'otcheritList',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/hospital/otcheritList.vue')
    },
    {
        path: '/ochred_view',
        name: 'ochred_view',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/TV_ochred/ochred_view.vue')
    },


    // <===== New for Balnetsa Qashqadaryo =======>

   
    
    {
        path: '/drop',
        name: 'drop',
        meta: { layout: 'empty' },
        component: () =>
            import ('../components/hospital/drop.vue')
    },
    
    
 


]




const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach((to, from, next) => {
    console.log(from)
    if (to.path != '/') {
        if (localStorage.Login != '') {
            if (localStorage.Type == 1) {
                if (to.path == '/bron' || to.path == '/medicineMin' || to.path == '/byuzi_date' || to.path == '/groupReport' || to.path == '/bydate' ||
                    to.path == '/medicine' || to.path == '/apply_admin' || to.path == '/return_report' || to.path == '/m_users' || to.path == '/bytime' || 
                    to.path == '/bycont_data' || to.path == '/info'  || to.path == '/biox'  || to.path == '/send_doc' || to.path == '/cashbox'
                    || to.path == '/bycontragent'  || to.path == '/dailyForPayment' || to.path == '/kunlik' || to.path == '/return_money'
                    || to.path == '/bonus_contragent' || to.path == '/payment_docReport' || to.path == '/bydoctorMoneyEarn' || to.path == '/bonus_repot' 
                    || to.path == '/bonus_full' || to.path == '/bydoctorMoneyEarn' || to.path == '/debitReport' 
                    || to.path == '/debitPayReport' || to.path == '/bytimeservice_bron' || to.path == '/pay_salary_list_all') {
                    next('/doctor')
                }
            } else if (localStorage.Type == 0) {
                next()
            } else if (localStorage.Type == 2) {
                if (to.path == '/doctor' || to.path == '/antigen' || to.path == '/bron' || to.path == '/medicineMin' ||
                    to.path == '/medicine' || to.path == '/m_users' || 
                    to.path == '/bycont_data' || to.path == '/result_lab' || to.path == '/info'  || to.path == '/biox'  || to.path == '/send_doc'
                    || to.path == '/bycontragent' || to.path == '/kunlik' 
                    || to.path == '/bonus_contragent' || to.path == '/bonus_repot' || to.path == '/bonus_full' || to.path == '/byuzi_date' 
                    || to.path == '/groupReport' || to.path == '/bydate' || to.path == '/payment_docReport' || to.path == '/bydoctorMoneyEarn'
                     || to.path == '/bytimeservice_bron' || to.path == '/pay_salary_list_all'
                    ) {
                    next('/cashbox')
                }
                // || to.path == '/return_money'
            } else if (localStorage.Type == 3) {
                if (to.path == '/bron' || to.path == '/medicineMin' || to.path == '/byuzi_date' || to.path == '/groupReport' || to.path == '/bydate' ||
                    to.path == '/medicine' || to.path == '/apply_admin' || to.path == '/return_report' || to.path == '/m_users' || 
                    to.path == '/bycont_data' || to.path == '/info'  || to.path == '/send_doc' || to.path == '/cashbox' || to.path == '/bytime'
                    || to.path == '/bycontragent' || to.path == '/dailyForPayment' || to.path == '/kunlik' || to.path == '/return_money'
                    || to.path == '/bonus_contragent' || to.path == '/bydoctorMoneyEarn' || to.path == '/payment_docReport'
                    || to.path == '/bonus_repot' || to.path == '/bonus_full'  || to.path == '/bydoctorMoneyEarn' 
                    || to.path == '/debitReport' || to.path == '/debitPayReport' || to.path == '/bytimeservice_bron' || to.path == '/pay_salary_list_all') {
                    next('/doctor')
                }
                // if (to.path == '/profil' || to.path == '/antigen' || to.path == '/selectShablon' || to.path == '/blankaPrint' ) {
                //     next()
                // } else if (to.path != '/doctor') {
                //     next('/doctor')
                // }
            } else if (localStorage.Type == 4) {
                if (to.path == '/cashbox' || to.path == '/antigen' || to.path == '/bron' || to.path == '/doctor' ||
                    to.path == '/word_search' || to.path == '/apply_admin' || to.path == '/return_report' ) {
                    next('/dailyForPayment')
                }
            }
            else if (localStorage.Type == 5) {
                if ( to.path == '/medicineMin' || to.path == '/byuzi_date' || to.path == '/groupReport' || to.path == '/bydate' ||
                    to.path == '/medicine' || to.path == '/return_report' || 
                    to.path == '/bycont_data' || to.path == '/info'  || to.path == '/biox'  || to.path == '/doctor'
                    || to.path == '/bycontragent'
                    || to.path == '/bonus_contragent' || to.path == '/bydoctorMoneyEarn' || to.path == '/bonus_repot' || 
                    to.path == '/payment_docReport' || to.path == '/bonus_full' || to.path == '/cashbox' || to.path == '/pay_salary_list_all'
                    || to.path == '/bytimeservice_bron' || to.path == '/pay_salary_list_all' || to.path == '/dailyForPayment' || to.path == '/kunlik' 
                    || to.path == '/debitReport' || to.path == '/debitPayReport' || to.path == '/return_money' || to.path == '/bron_time_payment' 
                    || to.path == '/apply_admin') {
                    next('/send_doc')
                    // || to.path == '/cashbox'
                }
            }
             else {
                next()
            }
            // else if (localStorage.Type == 2) {
            //     if (to.path != '/bemor' && to.path != '/doctor') {
            //         next('/bemor')
            //     }
            // }

            next()

        } else {
            next('/')
        }
    } else {
        next()
    }
})


// router.beforeEach((to, from, next) => {
//     console.log(from)
//     if (to.path != '/') {
//         if (localStorage.AuthId != null) {
//             next('/')
//         } else {
//             next(to)
//         }

//     } else {
//         next()
//     }
// })


export default router