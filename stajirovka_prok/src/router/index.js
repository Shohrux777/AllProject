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
    path: '/depart_connect_user',
    name: 'depart_connect_user',
    meta: { layout: 'main' },
    component: () => import( '../views/department/depart_connect_user.vue')
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
    path: '/topshiriqlar',
    name: 'topshiriqlar',
    meta: { layout: 'main' },
    component: () => import( '../views/topshiriqlar/topshiriqlar.vue')
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
  },
  {
    path: '/door',
    name: 'door',
    meta: { layout: 'main' },
    component: () => import( '../views/door/door.vue')
  },
  {
    path: '/device',
    name: 'device',
    meta: { layout: 'main' },
    component: () => import( '../views/device/device.vue')
  },
  {
    path: '/salary',
    name: 'salary',
    meta: { layout: 'main' },
    component: () => import( '../views/salary/salary.vue')
  },
  {
    path: '/userconnect_grafik',
    name: 'userconnect_grafik',
    meta: { layout: 'main' },
    component: () => import( '../views/connect/userconnect_grafik.vue')
  },
  {
    path: '/userconnect_salary',
    name: 'userconnect_salary',
    meta: { layout: 'main' },
    component: () => import( '../views/salary/userconnect_salary.vue')
  },
  {
    path: '/bytimeInOut',
    name: 'bytimeInOut',
    meta: { layout: 'main' },
    component: () => import( '../views/report/bytimeInOut.vue')
  },
  {
    path: '/bytimeInOutOne',
    name: 'bytimeInOutOne',
    meta: { layout: 'main' },
    component: () => import( '../views/report/bytimeInOutOne.vue')
  },
  {
    path: '/salaryReportbytime',
    name: 'salaryReportbytime',
    meta: { layout: 'main' },
    component: () => import( '../views/report/salaryReportbytime.vue')
  },
  {
    path: '/salaryReportbytimeNow',
    name: 'salaryReportbytimeNow',
    meta: { layout: 'main' },
    component: () => import( '../views/report/salaryReportbytimeNow.vue')
  },
  {
    path: '/qarovulxona_salary',
    name: 'qarovulxona_salary',
    meta: { layout: 'main' },
    component: () => import( '../views/report/qarovulxona_salary.vue')
  },
  {
    path: '/notComeInReport',
    name: 'notComeInReport',
    meta: { layout: 'main' },
    component: () => import( '../views/report/notComeInReport.vue')
  },
  {
    path: '/blockedUsers',
    name: 'blockedUsers',
    meta: { layout: 'main' },
    component: () => import( '../views/report/blockedUsers.vue')
  },
  {
    path: '/ComeInReport',
    name: 'ComeInReport',
    meta: { layout: 'main' },
    component: () => import( '../views/report/ComeInReport.vue')
  },
  {
    path: '/byvaqtReport',
    name: 'byvaqtReport',
    meta: { layout: 'main' },
    component: () => import( '../views/report/byvaqtReport.vue')
  },
  {
    path: '/come_in_out',
    name: 'come_in_out',
    meta: { layout: 'main' },
    component: () => import( '../views/report/come_in_out.vue')
  },
  {
    path: '/come_in_out_report',
    name: 'come_in_out_report',
    meta: { layout: 'empty' },
    component: () => import( '../views/report/come_in_out.vue')
  },
  {
    path: '/userAddRemoveDay',
    name: 'userAddDay',
    meta: { layout: 'main' },
    component: () => import( '../views/rasxod/userAddDay.vue')
  },
  {
    path: '/userRasxod',
    name: 'userRasxod',
    meta: { layout: 'main' },
    component: () => import( '../views/rasxod/userRasxod.vue')
  },
  {
    path: '/infopage',
    name: 'infopage',
    meta: { layout: 'empty' },
    component: () => import( '../views/hok_page/info_page.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
