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
        path: '/about',
        name: 'about',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/About.vue')
    },
    
    {
        path: '/currency',
        name: 'currency',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/valyuta/currency.vue')
    },
    {
        path: '/order',
        name: 'order',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/order/order.vue')
    },
    {
        path: '/order_add/:id',
        name: 'order_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/order/order_add.vue')
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
        path: '/sensor',
        name: 'sensor',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/archieve/sensor.vue')
    },
    {
        path: '/addcolor',
        name: 'addcolor',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/archieve/color.vue')
    },
    {
        path: '/scr_add',
        name: 'scr_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/archieve/scr.vue')
    },
    {
        path: '/m_product',
        name: 'm_product',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product.vue')
    },
    {
        path: '/m_product_add/:id',
        name: 'm_product_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product_add.vue')
    },
    {
        path: '/m_product_group',
        name: 'm_product_group',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product_group.vue')
    },
    {
        path: '/m_product_group_add/:id',
        name: 'm_product_group_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product_group_add.vue')
    },
    {
        path: '/m_product_conf',
        name: 'm_product_conf',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product_conf.vue')
    },
    {
        path: '/m_product_conf_add/:id',
        name: 'm_product_conf_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/product/m_product_conf_add.vue')
    },
    {
        path: '/market_index',
        name: 'market_index',
        meta: { layout: 'login' },
        component: () =>
            import ('../views/market/market_index.vue')
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
        path: '/limit',
        name: 'limit',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/limit/limit.vue')
    },
    {
        path: '/m_bytime',
        name: 'm_bytime',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/market/report/bytime.vue')
    },
    {
        path: '/m_bydate',
        name: 'm_bydate',
        meta: { layout: 'hospital' },
        component: () =>
            import ('../views/market/report/bydate.vue')
    },
    {
        path: '/limit_add/:id',
        name: 'limit_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/limit/limit_add.vue')
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
        path: '/m_invoice',
        name: 'm_invoice',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/invoice/invoice.vue')
    },
    {
        path: '/invoice_add/:id',
        name: 'invoice_add',
        meta: { layout: 'regis' },
        component: () =>
            import ('../views/market/invoice/invoice_add.vue')
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
        path: '/biox',
        name: 'bioxpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/biox.vue')
    },
    {
        path: '/biox_add/:id',
        name: 'biox',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/biox_add.vue')
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
        path: '/ExpressBC',
        name: 'ExpressBCpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/ExpressBC.vue')
    },
    {
        path: '/ExpressBC_add/:id',
        name: 'ExpressBC',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/ExpressBC_add.vue')
    },
    {
        path: '/gepatitBC',
        name: 'gepatitBCpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/gepatitBC.vue')
    },
    {
        path: '/gepatitBC_add/:id',
        name: 'gepatitBC',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/gepatitBC_add.vue')
    },
    {
        path: '/koagulogramma',
        name: 'koagulogrammapage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/koagulogramma.vue')
    },
    {
        path: '/koagulogramma_add/:id',
        name: 'koagulogramma',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/koagulogramma_add.vue')
    },
    {
        path: '/kalogramma',
        name: 'kalogrammapage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/kolagramma.vue')
    },
    {
        path: '/kalogramma_add/:id',
        name: 'kalogramma',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/kolagramma_add.vue')
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
        path: '/nechiporenko',
        name: 'nechiporenkopage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/nechiporenko.vue')
    },
    {
        path: '/nechiporenko_add/:id',
        name: 'nechiporenko',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/nechiporenko_add.vue')
    },
    {
        path: '/onkomarker',
        name: 'onkomarkerpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/onkomarker.vue')
    },
    {
        path: '/onkomarker_add/:id',
        name: 'onkomarker',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/onkomarker_add.vue')
    },
    {
        path: '/peshob',
        name: 'peshobpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/peshob.vue')
    },
    {
        path: '/peshob_add/:id',
        name: 'peshob',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/peshob_add.vue')
    },
    {
        path: '/garmonlarQon',
        name: 'garmonlarQonpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/garmonlarQon.vue')
    },
    {
        path: '/garmonlarQon_add/:id',
        name: 'garmonlarQon',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/garmonlarQon_add.vue')
    },
    {
        path: '/remaproba',
        name: 'remaprobapage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/remaproba.vue')
    },
    {
        path: '/remaproba_add/:id',
        name: 'remaproba',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/remaproba_add.vue')
    },
    {
        path: '/rv',
        name: 'rvpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/rv.vue')
    },
    {
        path: '/rv_add/:id',
        name: 'rv',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/rv_add.vue')
    },
    {
        path: '/torch',
        name: 'torchpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/torch.vue')
    },
    
    {
        path: '/torch_add/:id',
        name: 'torch',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/torch_add.vue')
    },
    {
        path: '/spirma',
        name: 'spirmapage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/spirma.vue')
    },
    
    {
        path: '/spirma_add/:id',
        name: 'spirma',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/spirma_add.vue')
    },

    {
        path: '/um_qon',
        name: 'um_qonpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/um_qon.vue')
    },
    
    
    {
        path: '/um_qon_add/:id',
        name: 'um_qon',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/um_qon_add.vue')
    },
    {
        path: '/umum_qon2',
        name: 'umum_qon2page',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/umum_qon2.vue')
    },
    
    
    {
        path: '/umum_qon_add2/:id',
        name: 'umum_qon_add2',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/umum_qon_add2.vue')
    },
    {
        path: '/vsk',
        name: 'vskpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vsk.vue')
    },
    
    {
        path: '/vsk_add/:id',
        name: 'vsk',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vsk_add.vue')
    },
    {
        path: '/gruppa_kolvo',
        name: 'gruppa_kolvopage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/gruppa_kolvo.vue')
    },
    {
        path: '/vitD',
        name: 'vitDpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vitD.vue')
    },
    
    {
        path: '/vitD_add/:id',
        name: 'vitD',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vitD_add.vue')
    },
    {
        path: '/vitD_print/:id',
        name: 'vitD_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/vitD_print.vue')
    },
    {
        path: '/vitD_show/:id',
        name: 'vitD_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/vitD_print.vue')
    },

    {
        path: '/antiTpo',
        name: 'antiTpopage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/antiTpo.vue')
    },
    
    {
        path: '/antiTpo_add/:id',
        name: 'antiTpo',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/antiTpo_add.vue')
    },
    {
        path: '/antiTpo_print/:id',
        name: 'antiTpo_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/antiTpo_print.vue')
    },
    {
        path: '/antiTpo_show/:id',
        name: 'antiTpo_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/antiTpo_print.vue')
    },
    
    {
        path: '/gruppa_kolvo_add/:id',
        name: 'gruppa_kolvo',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/gruppa_kolvo_add.vue')
    },

    {
        path: '/obshey_krov',
        name: 'obshey_krovpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/obshey_krov.vue')
    },
    {
        path: '/obshey_krov_add/:id',
        name: 'obshey_krov',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/obshey_krov_add.vue')
    },
    {
        path: '/qon_garmon',
        name: 'qon_garmonpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/qon_garmon.vue')
    },
    {
        path: '/qon_garmon_add/:id',
        name: 'qon_garmon',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/qon_garmon_add.vue')
    },
    {
        path: '/biox_krov',
        name: 'biox_krovpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/biox_krov.vue')
    },
    {
        path: '/biox_krov_add/:id',
        name: 'biox_krov',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/biox_krov_add.vue')
    },
    {
        path: '/krov_kolvo',
        name: 'krov_kolvopage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/krov_kolvo.vue')
    },
    {
        path: '/krov_kolvo_add/:id',
        name: 'krov_kolvo',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/krov_kolvo_add.vue')
    },
    {
        path: '/spid',
        name: 'spidpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/spid.vue')
    },
    {
        path: '/spid_add/:id',
        name: 'spid',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/spid_add.vue')
    },
    {
        path: '/vsk_sux',
        name: 'vsk_suxpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vsk_sux.vue')
    },
    {
        path: '/vsk_sux_add/:id',
        name: 'vsk_sux',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/vsk_sux_add.vue')
    },
    {
        path: '/lipid_spek',
        name: 'lipid_spekpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/lipid_spek.vue')
    },
    {
        path: '/lipid_spek_add/:id',
        name: 'lipid_spek',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/lipid_spek_add.vue')
    },
    {
        path: '/glik_gemok',
        name: 'glik_gemokpage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/glik_gemok.vue')
    },
    {
        path: '/glik_gemok_add/:id',
        name: 'glik_gemok',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/glik_gemok_add.vue')
    },
    {
        path: '/obshey_mochi',
        name: 'obshey_mochipage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/obshey_mochi.vue')
    },
    {
        path: '/obshey_mochi_add/:id',
        name: 'obshey_mochi',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/obshey_mochi_add.vue')
    },
    {
        path: '/mazok_step',
        name: 'mazok_steppage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok_step.vue')
    },
    {
        path: '/mazok_step_add/:id',
        name: 'mazok_step',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok_step_add.vue')
    },
    {
        path: '/mazok_10',
        name: 'mazok_10page',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok_10.vue')
    },
    {
        path: '/mazok_10_add/:id',
        name: 'mazok_10',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/mazok_10_add.vue')
    },

    


    {
        path: '/biox_print/:id',
        name: 'biox_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/biox_print.vue')
    },
    {
        path: '/bck_print/:id',
        name: 'bck_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/bck_print.vue')
    },
    {
        path: '/covidExpress_print/:id',
        name: 'covidExpress_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/covidExpress_print.vue')
    },
    {
        path: '/covid19_print/:id',
        name: 'covid19_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/covid19_print.vue')
    },
    {
        path: '/expressBC_print/:id',
        name: 'expressBC_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/expressBC_print.vue')
    },
    {
        path: '/garmonlarQon_print/:id',
        name: 'garmonlarQon_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/garmonlarQon_print.vue')
    },
    {
        path: '/gepatitBC_print/:id',
        name: 'gepatitBC_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/gepatitBC_print.vue')
    },
    {
        path: '/kolagramma_print/:id',
        name: 'kolagramma_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/kolagramma_print.vue')
    },
    {
        path: '/obshey_mochi_print/:id',
        name: 'obshey_mochi_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/obshey_mochi_print.vue')
    },
    {
        path: '/gruppa_kolvo_print/:id',
        name: 'gruppa_kolvo_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/gruppa_kolvo_print.vue')
    },
    {
        path: '/mazok_print/:id',
        name: 'mazok_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/mazok_print.vue')
    },
    {
        path: '/nechiporenko_print/:id',
        name: 'nechiporenko_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/nechiporenko_print.vue')
    },
    {
        path: '/onkomarker_print/:id',
        name: 'onkomarker_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/onkomarker_print.vue')
    },
    {
        path: '/peshob_print/:id',
        name: 'peshob_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/peshob_print.vue')
    },
    {
        path: '/remaproba_print/:id',
        name: 'remaproba_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/remaproba_print.vue')
    },
    {
        path: '/rv_print/:id',
        name: 'rv_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/rv_print.vue')
    },
    {
        path: '/torch_print/:id',
        name: 'torch_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/torch_print.vue')
    },
    {
        path: '/um_qon_print/:id',
        name: 'um_qon_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/um_qon_print.vue')
    },
    {
        path: '/umum_qon_print2/:id',
        name: 'umum_qon_print2',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/umum_qon_print2.vue')
    },
    {
        path: '/spirma_print/:id',
        name: 'spirma_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/spirma_print.vue')
    },
    {
        path: '/kolagram_print/:id',
        name: 'kolagram_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/kolagram_print.vue')
    },
    {
        path: '/obshey_krov_print/:id',
        name: 'kolagram_printId',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/obshey_krov_print.vue')
    },
    {
        path: '/qon_garmon_print/:id',
        name: 'qon_garmon_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/qon_garmon_print.vue')
    },
    {
        path: '/biox_krov_print/:id',
        name: 'biox_krov_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/biox_krov_print.vue')
    },
    {
        path: '/krov_kolvo_print/:id',
        name: 'krov_kolvo_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/krov_kolvo_print.vue')
    },
    {
        path: '/spid_print/:id',
        name: 'spid_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/spid_print.vue')
    },
    {
        path: '/vsk_sux_print/:id',
        name: 'vsk_sux_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/vsk_sux_print.vue')
    },
    {
        path: '/lipid_spek_print/:id',
        name: 'lipid_spek_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/lipid_spek_print.vue')
    },
    {
        path: '/glik_gemok_print/:id',
        name: 'glik_gemok_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/glik_gemok_print.vue')
    },
    {
        path: '/mazok_step_print/:id',
        name: 'mazok_step_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/mazok_step_print.vue')
    },
    {
        path: '/mazok_10_print/:id',
        name: 'mazok_10_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/laboratory/mazok_10_print.vue')
    },


    

    {
        path: '/biox_show/:id',
        name: 'biox_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/biox_print.vue')
    },
    {
        path: '/vsk_show/:id',
        name: 'vsk_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/bck_print.vue')
    },
    {
        path: '/covidExpress_show/:id',
        name: 'covidExpress_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/covidExpress_print.vue')
    },
    {
        path: '/covidQon_show/:id',
        name: 'covidQon_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/covid19_print.vue')
    },
    {
        path: '/ExpressBC_show/:id',
        name: 'ExpressBC_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/expressBC_print.vue')
    },
    {
        path: '/garmonlarQon_show/:id',
        name: 'garmonlarQon_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/garmonlarQon_print.vue')
    },
    {
        path: '/gepatitBC_show/:id',
        name: 'gepatitBC_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/gepatitBC_print.vue')
    },
    {
        path: '/koagulogramma_show/:id',
        name: 'koagulogramma_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/kolagramma_print.vue')
    },
    {
        path: '/mazok_show/:id',
        name: 'mazok_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/mazok_print.vue')
    },
    {
        path: '/nechiporenko_show/:id',
        name: 'nechiporenko_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/nechiporenko_print.vue')
    },
    {
        path: '/onkomarker_show/:id',
        name: 'onkomarker_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/onkomarker_print.vue')
    },
    {
        path: '/peshob_show/:id',
        name: 'peshob_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/peshob_print.vue')
    },
    {
        path: '/remaproba_show/:id',
        name: 'remaproba_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/remaproba_print.vue')
    },
    {
        path: '/rv_show/:id',
        name: 'rv_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/rv_print.vue')
    },
    {
        path: '/torch_show/:id',
        name: 'torch_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/torch_print.vue')
    },
    {
        path: '/um_qon_show/:id',
        name: 'um_qon_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/um_qon_print.vue')
    },
    {
        path: '/umum_qon2_show/:id',
        name: 'umum_qon2_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/umum_qon_print2.vue')
    },
    {
        path: '/spirma_show/:id',
        name: 'spirma_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/spirma_print.vue')
    },
    {
        path: '/kalogramma_show/:id',
        name: 'kolagram_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/kolagram_print.vue')
    },
    {
        path: '/mazok_step_show/:id',
        name: 'mazok_step_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/mazok_step_print.vue')
    },
    {
        path: '/obshey_krov_show/:id',
        name: 'obshey_krov_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/obshey_krov_print.vue')
    },
    {
        path: '/qon_garmon_show/:id',
        name: 'qon_garmon_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/qon_garmon_print.vue')
    },
    {
        path: '/biox_krov_show/:id',
        name: 'biox_krov_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/biox_krov_print.vue')
    },
    {
        path: '/krov_kolvo_show/:id',
        name: 'krov_kolvo_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/krov_kolvo_print.vue')
    },
    {
        path: '/spid_show/:id',
        name: 'spid_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/spid_print.vue')
    },
    {
        path: '/vsk_sux_show/:id',
        name: 'vsk_sux_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/vsk_sux_print.vue')
    },
    {
        path: '/lipid_spek_show/:id',
        name: 'lipid_spek_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/lipid_spek_print.vue')
    },
    {
        path: '/glik_gemok_show/:id',
        name: 'glik_gemok_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/glik_gemok_print.vue')
    },
    {
        path: '/obshey_mochi_show/:id',
        name: 'obshey_mochi_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/obshey_mochi_print.vue')
    },
    {
        path: '/gruppa_kolvo_show/:id',
        name: 'gruppa_kolvo_show',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/gruppa_kolvo_print.vue')
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
        path: '/opuxoloviya',
        name: 'opuxoloviyapage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/opuxoloviya.vue')
    },
    {
        path: '/opuxoloviya_add/:id',
        name: 'opuxoloviya',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/opuxoloviya_add.vue')
    },
    {
        path: '/opuxoloviya_show/:id',
        name: 'opuxoloviya_print',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/showLaboratory/opuxoloviya_print.vue')
    },
    {
        path: '/bioximechisky',
        name: 'bioximechiskypage',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/bioximechisky.vue')
    },
    {
        path: '/bioximechisky_add/:id',
        name: 'bioximechisky',
        meta: { layout: 'analysis' },
        component: () =>
            import ('../views/analysis/bioximechisky_add.vue')
    },
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
                    || to.path == '/bonus_contragent' || to.path == '/payment_docReport' || to.path == '/bydoctorMoneyEarn' || to.path == '/bonus_repot' || to.path == '/bonus_full' || to.path == '/bydoctorMoneyEarn' || to.path == '/debitReport' || to.path == '/debitPayReport') {
                    next('/doctor')
                }
            } else if (localStorage.Type == 0) {
                next()
            } else if (localStorage.Type == 2) {
                if (to.path == '/doctor' || to.path == '/antigen' || to.path == '/bron' || to.path == '/medicineMin' ||
                    to.path == '/medicine'  || to.path == '/return_report' || to.path == '/m_users' || 
                    to.path == '/bycont_data' || to.path == '/result_lab' || to.path == '/info'  || to.path == '/biox'  || to.path == '/send_doc'
                    || to.path == '/bycontragent' || to.path == '/kunlik' || to.path == '/return_money'
                    || to.path == '/bonus_contragent' || to.path == '/bonus_repot' || to.path == '/bonus_full' || to.path == '/byuzi_date' 
                    || to.path == '/groupReport' || to.path == '/bydate' || to.path == '/payment_docReport' || to.path == '/bydoctorMoneyEarn') {
                    next('/cashbox')
                }
            } else if (localStorage.Type == 3) {
                if (to.path == '/bron' || to.path == '/medicineMin' || to.path == '/byuzi_date' || to.path == '/groupReport' || to.path == '/bydate' ||
                    to.path == '/medicine' || to.path == '/apply_admin' || to.path == '/return_report' || to.path == '/m_users' || 
                    to.path == '/bycont_data' || to.path == '/info'  || to.path == '/send_doc' || to.path == '/cashbox' || to.path == '/bytime'
                    || to.path == '/bycontragent' || to.path == '/dailyForPayment' || to.path == '/kunlik' || to.path == '/return_money'
                    || to.path == '/bonus_contragent' || to.path == '/bydoctorMoneyEarn' || to.path == '/payment_docReport' || to.path == '/bonus_repot' || to.path == '/bonus_full'  || to.path == '/bydoctorMoneyEarn' || to.path == '/debitReport' || to.path == '/debitPayReport') {
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
                    || to.path == '/bycontragent' || to.path == '/bytime' ||
                     to.path == '/kunlik' || to.path == '/return_money'
                    || to.path == '/bonus_contragent' || to.path == '/bydoctorMoneyEarn' || to.path == '/bonus_repot' || 
                    to.path == '/payment_docReport' || to.path == '/bonus_full') {
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