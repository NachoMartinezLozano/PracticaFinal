<script setup>

  import { ref } from 'vue'
  import Cabecera from './components/Cabecera.vue'
  import Navegador from './components/Navegador.vue'
  import { useVisibilidadNavegador } from './composables/useVisibilidadNavegador'

  const { showNavegador, toggleNavegador } = useVisibilidadNavegador()

</script>

<template>
  <div class="flex flex-col min-h-screen">
    <Cabecera />
    <div >
      <Navegador v-show="showNavegador" />
      <main>
         <RouterView />
      </main>
    </div>
  </div>
</template>

<style scoped>

</style>
