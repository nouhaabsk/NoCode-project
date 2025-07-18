import { createRouter, createWebHistory } from 'vue-router'
import Login from '../components/Login.vue'
import Signup from '../components/Signup.vue'
import Dashboard from '../components/Dashboard.vue'
import About from '../components/About.vue';
import Profile from '@/components/Profile'


const routes = [
  {
    path: '/',
    redirect: '/login'  
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/signup',
    name: 'Signup',
    component: Signup
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard,
    meta: { requiresAuth: true } // Nécessite une authentification
  },
   {
    path: '/a-propos',
    name: 'AboutPage',
    component: About
  },
  {
  path: '/dashboard/poc',
  component: () => import('@/components/poc/POCWrapper.vue'),
  meta: { requiresAuth: true }
},
{
    path: '/profile',
    name: 'Profile-Page',
    component: Profile,
    meta: { requiresAuth: true } // Si vous voulez protéger cette route
  }
  

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router