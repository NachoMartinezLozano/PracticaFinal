<script setup>

  import { ref } from 'vue'
  import Cabecera from './components/Cabecera.vue'
  import Navegador from './components/Navegador.vue'
  import { useVisibilidadNavegador } from './composables/useVisibilidadNavegador'

  const { showNavegador } = useVisibilidadNavegador()

</script>

<template>
  <div class="flex flex-col min-h-screen">
    <Cabecera />
    <div class="flex flex-row">
      <Navegador v-if="showNavegador" />
      <main :class="['flex-1 p-4', showNavegador ? 'ml-45' : 'ml-0']">
         <RouterView />
      </main>
    </div>
  </div>
</template>

<style scoped>

</style>
