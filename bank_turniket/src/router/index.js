import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/login/login.vue'

const routes = [
  {
    path: '/',
    name: 'login',
    meta: { layout: 'login' },
    component: LoginView
  },
  {
    path: '/about',
    name: 'about',
    meta: { layout: 'main' },
    component: () => import( '../views/AboutView.vue')
  },
  {
    path: '/aboutweb',
    name: 'aboutweb',
    meta: { layout: 'main' },
    component: () => import( '../views/AboutView.vue')
  },
  {
    path: '/department',
    name: 'department',
    meta: { layout: 'main' },
    component: () => import( '../views/department/department.vue')
  },
  {
    path: '/company',
    name: 'company',
    meta: { layout: 'main' },
    component: () => import( '../views/company/company.vue')
  },
  {
    path: '/user',
    name: 'user',
    meta: { layout: 'main' },
    component: () => import( '../views/user/user.vue')
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    meta: { layout: 'main' },
    component: () => import( '../views/dashboard/checkinOut.vue')
  },
  {
    path: '/smena',
    name: 'smena',
    meta: { layout: 'main' },
    component: () => import( '../views/grafik/smena.vue')
  },
  {
    path: '/ish_grafigi',
    name: 'ish_grafigi',
    meta: { layout: 'main' },
    component: () => import( '../views/grafik/ish_grafigi.vue')
  },
  {
    path: '/result_gr',
    name: 'result_gr',
    meta: { layout: 'main' },
    component: () => import( '../views/grafik/result_gr.vue')
  },
  {
    path: '/connect_user',
    name: 'connect_user',
    meta: { layout: 'main' },
    component: () => import( '../views/grafik/connect_user.vue')
  },
  {
    path: '/test',
    name: 'test',
    meta: { layout: 'main' },
    component: () => import( '../views/dashboard/test.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
