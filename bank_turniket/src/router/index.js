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
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router