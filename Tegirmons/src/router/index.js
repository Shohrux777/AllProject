import Vue from 'vue'
import VueRouter from 'vue-router'


Vue.use(VueRouter)

const routes = [{
        path: '/',
        name: 'login',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/login/login.vue')
    },
    {
        path: '/company',
        name: 'company',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/company/company.vue')
    },
    {
        path: '/company_add/:id',
        name: 'company_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/company/company_Add.vue')
    },
    {
        path: '/door',
        name: 'door',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/device/device.vue')
    },
    {
        path: '/door_add/:id',
        name: 'door_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/device/device_add.vue')
    },
    {
        path: '/doors',
        name: 'doors',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/door/door.vue')
    },
    {
        path: '/doors_add/:id',
        name: 'doors_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/door/door_add.vue')
    },
    {
        path: '/userFace',
        name: 'userFace',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/user_face/userFace.vue')
    },
    {
        path: '/userFace_add/:id',
        name: 'userFace_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/user_face/userFace_Add.vue')
    },
    {
        path: '/dashboard',
        name: 'dashboard',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/face_control/checkinOut.vue')
    },
    {
        path: '/product',
        name: 'product',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/product/product.vue')
    },
    {
        path: '/product_add/:id',
        name: 'product_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/product/product_Add.vue')
    },
    {
        path: '/contragent',
        name: 'contragent',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/contragent/contragent.vue')
    },
    {
        path: '/contragent_add/:id',
        name: 'contragent_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/contragent/contragent_Add.vue')
    },
    {
        path: '/district',
        name: 'district',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/district/district.vue')
    },
    {
        path: '/district_add/:id',
        name: 'district_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/district/district_Add.vue')
    },
    {
        path: '/measurment',
        name: 'measurment',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/measurment/measurment.vue')
    },
    {
        path: '/measurment_Add/:id',
        name: 'measurment_Add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/measurment/measurment_Add.vue')
    },
    {
        path: '/kassa',
        name: 'kassa',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/kassa/kassa.vue')
    },
    {
        path: '/kassa_Add/:id',
        name: 'kassa_Add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/kassa/kassa_Add.vue')
    },
    {
        path: '/hisob',
        name: 'hisob',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/hisob/hisob.vue')
    },
    {
        path: '/hisob_Add/:id',
        name: 'hisob_Add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/hisob/hisob_Add.vue')
    },
    {
        path: '/user',
        name: 'user',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/user/user.vue')
    },
    {
        path: '/user_add/:id',
        name: 'user_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/user/user_Add.vue')
    },
    {
        path: '/department',
        name: 'department',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/department/department.vue')
    },
    {
        path: '/department_add/:id',
        name: 'department_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/department/department_Add.vue')
    },
    {
        path: '/client',
        name: 'client',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/client/client.vue')
    },
    {
        path: '/client_add/:id',
        name: 'client_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/client/client_Add.vue')
    },
    {
        path: '/worker',
        name: 'worker',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/worker/worker.vue')
    },
    {
        path: '/worker_add/:id',
        name: 'worker_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/worker/worker_Add.vue')
    },
    {
        path: '/webcam',
        name: 'webcam',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/webcam/webcam.vue')
    },
    {
        path: '/client_controler_add/:id',
        name: 'client_controler_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/client/client_controler_Add.vue')
    },
    {
        path: '/client_controler',
        name: 'client_controler',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/client/client_controler.vue')
    },
    
       
    {
        path: '/authorization/:id',
        name: 'authorization',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/user/authorization.vue')
    },
    


    // <----    Tegirmon  ---->

    {
        path: '/getProduct',
        name: 'getProduct',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/getProduct.vue')
    },
    {
        path: '/changeProduct',
        name: 'changeProduct',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/changeProduct.vue')
    },
    {
        path: '/buy',
        name: 'buy',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/buy/buy.vue')
    },
    {
        path: '/buy_add/:id',
        name: 'buy_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/buy/buy_Add.vue')
    },
    
    {
        path: '/changing',
        name: 'changing',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/changing/changing.vue')
    },
    {
        path: '/changing_add/:id',
        name: 'changing_add',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/changing/changing_Add.vue')
    },
    {
        path: '/tarozi',
        name: 'tarozi',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/tarozi.vue')
    },
    {
        path: '/today_comingList',
        name: 'today_comingList',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/today_comingList.vue')
    },
    {
        path: '/changeScore/:id',
        name: 'changeScore',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/changeScore.vue')
    },
    {
        path: '/zaxiraniAlmashtirish/',
        name: 'zaxiraniAlmashtirish',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/zaxiraniAlmashtirish.vue')
    },
    {
        path: '/main_kassa',
        name: 'main_kassa',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/main_kassa/main_kassa.vue')
    },
    {
        path: '/hisoblar',
        name: 'hisoblar',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/main_kassa/hisoblar.vue')
    },
    {
        path: '/savdo_all_kassa_info',
        name: 'savdo_all_kassa_info',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/main_kassa/savdo_all_kassa_info.vue')
    },
    {
        path: '/main_kassa_report',
        name: 'main_kassa_report',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/main_kassa/main_kassa_report.vue')
    },
    {
        path: '/sell',
        name: 'sell',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/sell/sell.vue')
    },
    {
        path: '/cash',
        name: 'cash',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/sell/closeCash.vue')
    },
    {
        path: '/setting',
        name: 'setting',
        meta: { layout: 'empty' },
        component: () =>
            import ('../views/setting/setting.vue')
    },
    {
        path: '/today_groups',
        name: 'today_groups',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/today_groups.vue')
    },
    {
        path: '/showGroupDetail/:id',
        name: 'showGroupDetail',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/showGroupDetail.vue')
    },
    
    {
        path: '/showGroupDetail_accept/:id',
        name: 'showGroupDetail_accept',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/showGroupDetail_accept.vue')
    },
    // {
    //     path: '/showRuyxatItem/:id',
    //     name: 'showRuyxatItem',
    //     meta: { layout: 'navbar' },
    //     component: () =>
    //         import ('../views/mainPage/showRuyxatItem.vue')
    // },
    {
        path: '/getProductFromGroup',
        name: 'getProductFromGroup',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/getProductFromGroup.vue')
    },
    {
        path: '/groupsAccepted',
        name: 'groupsAccepted',
        meta: { layout: 'navbar' },
        component: () =>
            import ('../views/mainPage/groupsAccepted.vue')
    },
    {
        path: '/rasxod',
        name: 'rasxod',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/rasxod.vue')
    },
    {
        path: '/worker_rasxod',
        name: 'worker_rasxod',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/worker_rasxod.vue')
    },

    {
        path: '/getProduct_report',
        name: 'getProduct_report',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/getProduct_report.vue')
    },
    {
        path: '/zaxiraSend',
        name: 'zaxiraSend',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/zaxiraSend.vue')
    },
    {
        path: '/usedProductList',
        name: 'usedProductList',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/usedProductList.vue')
    },
    {
        path: '/usedProductListDetails',
        name: 'usedProductListDetails',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/usedProductListDetails.vue')
    },
    {
        path: '/zaxiraList',
        name: 'zaxiraList',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/zaxiraList.vue')
    },
    {
        path: '/chiqarilPulOlish',
        name: 'chiqarilPulOlish',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/chiqarilPulOlish.vue')
    },
    {
        path: '/changeWithoutReg',
        name: 'changeWithoutReg',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/changeWithoutReg.vue')
    },
    {
        path: '/millUndo',
        name: 'millUndo',
        meta: { layout: 'main' },
        component: () =>
            import ('../views/report/millUndo.vue')
    },
    {
        path: '/tarozi_list',
        name: 'tarozi_list',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/tarozi_list.vue')
    },
    {
        path: '/client_rashshod',
        name: 'client_rashshod',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/client_rashshod.vue')
    },
    {
        path: '/big_report',
        name: 'big_report',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/big_report.vue')
    },
    {
        path: '/rashshod_list',
        name: 'rashshod_list',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/rashshod_list.vue')
    },
    {
        path: '/rashshod_qilingan',
        name: 'rashshod_qilingan',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/rashshod_qilingan.vue')
    },
    {
        path: '/add_tarozi',
        name: 'add_tarozi',
        meta: { layout: 'bigtarozi' },
        component: () =>
            import ('../views/big_tarozi/add_tarozi.vue')
    },
    {
        path: '/add_last_tarozi/:id',
        name: 'add_last_tarozi',
        meta: { layout: 'bigtarozi'},
        component: () =>
            import ('../views/big_tarozi/add_last_tarozi.vue')
    },
    {
        path: '/add_rashshod/:id',
        name: 'add_rashshod',
        meta: { layout: 'bigtarozi'},
        component: () =>
            import ('../views/big_tarozi/add_rashshod.vue')
    },
    {
        path: '/add_rashshod_bulgan/:id',
        name: 'add_rashshod_bulgan',
        meta: { layout: 'bigtarozi'},
        component: () =>
            import ('../views/big_tarozi/add_rashshod_bulgan.vue')
    },
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach((to, from, next) => {
    if (to.path != '/') {
        if (localStorage.Login != '') {
            // if (localStorage.AccessType == 1) {
            //     if (to.path == '/rasxod' || to.path == '/getProduct_report' || to.path == '/zaxiraSend'
            //      || to.path == '/changeWithoutReg' || to.path == '/millUndo' || to.path == '/zaxiraList' ) {
            //         next('/zaxiraniAlmashtirish')
            //     }
            // } else 
            if (localStorage.AccessType == 0) {
                next()
            } else if (localStorage.AccessType == 2) {
                if (to.path == '/sell' || to.path == '/zaxiraniAlmashtirish' || to.path == '/product' || to.path == '/zaxiraList') {
                    next('/tarozi')
                }
            }
             else {
                next()
            }

            next()

        } else {
            next('/')
        }
    } else {
        next()
    }
})

export default router