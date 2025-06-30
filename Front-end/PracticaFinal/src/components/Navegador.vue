<!--  Clase para el navegador lateral de la aplicación -->

<script setup>

  import { useRouter } from 'vue-router'

  const router = useRouter()

</script>

<template>

  <div class="navbar bg-base-100 shadow-sm flex flex-col w-45 h-[calc(100vh-64px)] fixed top-[64px] left-0">
     <div class="flex-1 flex flex-col justify-center">
      <ul class="menu menu-vertical px-4 space-y-2">
        <li><button class="btn" @click="router.push({ path: '/operaciones'})">Operaciones</button></li>
        <li><button class="btn" @click="router.push({ path: '/equipos'})">Equipos</button></li>
        <li><button class="btn" @click="router.push({ path: '/agentes'})">Agentes</button></li>
      </ul>
    </div>
  </div>

</template>

<style>

</style>