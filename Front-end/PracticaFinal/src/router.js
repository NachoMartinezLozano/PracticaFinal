
import { createMemoryHistory, createRouter } from 'vue-router'

const routes = [
  { path: '/', component: import('./pages/PaginaPrincipal.vue') },
  { path: '/operaciones', component: import('./pages/PaginaOperaciones.vue')},
  { path: '/equipos', component: import('./pages/PaginaEquipos.vue') },
  { path: '/agentes', component: import('./pages/PaginaAgentes.vue') },
] 

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router