<!--  Clase para el navegador lateral de la aplicación -->

<script setup>

  import { useRouter } from 'vue-router'
  import { useVisibilidadNavegador } from '../composables/useVisibilidadNavegador';

  const router = useRouter()
  const { showNavegador, toggleNavegador } = useVisibilidadNavegador()

  const navigateAndClose = (path) => {
    router.push({ path })
    toggleNavegador()
  }

</script>

<template>

  <div class="bg-base-100 flex flex-col w-45 h-[calc(100vh-64px)] fixed top-[64px] left-0 transition-transform duration-300 z-[1000]" :class="{ 'translate-x-0': showNavegador, '-translate-x-full': !showNavegador }">
     <div class="flex-1 flex flex-col justify-center">
      <ul class="menu menu-vertical px-4 space-y-2">
        <li><button class="btn btn-soft btn-info" @click="navigateAndClose('/operaciones')">Operaciones</button></li>
        <li><button class="btn btn-soft btn-info" @click="navigateAndClose('/equipos')">Equipos</button></li>
        <li><button class="btn btn-soft btn-info" @click="navigateAndClose('/agentes')">Agentes</button></li>
      </ul>
    </div>
  </div>

</template>

<style scoped>

</style>