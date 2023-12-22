import { createApp } from 'vue'
// import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import router from './router' 

// import EmployeeList from './view/employee/EmployeeList.vue'
// import RepostList from './view/repost/RepostList.vue'
// const routes = [
//     // { path: '/', component: Home },
//     { path: '/employee', component: EmployeeList },
//     { path: '/repost', component: RepostList },
//   ]

//   const router = createRouter({
//     history: createWebHistory(process.env.BASE_URL),
//     routes
//   })


createApp(App).use(router).mount('#app')
