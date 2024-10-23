import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/',
    name: 'login',
    meta: { layout: 'empty' },
    component: () => import( '../views/main/login.vue')
  },
  {
    path: '/send_doc',
    name: 'send_doc',
    meta: { layout: 'empty' },
    component: () => import( '../views/main/send_doctor.vue')
  },
  
  {
    path: '/result_lab',
    name: 'result_lab',
    meta: { layout: 'empty' },
    component: () => import( '../views/main/result_lab.vue')
  },
  {
    path: '/lab_printed/:id',
    name: 'lab_printed',
    meta: { layout: 'empty' },
    component: () =>
        import ('../views/main/lab_printed.vue')
  },
  {
    path: '/hospital_lab_result/:id',
    name: 'hospital_lab_result',
    meta: { layout: 'empty' },
    component: () =>
        import ('../views/HomeView.vue')
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
