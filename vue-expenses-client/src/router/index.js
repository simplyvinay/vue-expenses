import Vue from 'vue'
import VueRouter from 'vue-router'
import Dashboard from '@/views/Dashboard.vue'
import Expenses from '@/views/Expenses.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Dashboard
  },
  {
    path: '/expenses',
    name: 'expenses',
    component: Expenses
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router