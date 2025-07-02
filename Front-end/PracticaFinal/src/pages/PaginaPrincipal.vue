<script setup>
    import { ref, onMounted, onUnmounted} from 'vue'
    import Cabecera from '../components/Cabecera.vue'
    import Navegador from '../components/Navegador.vue'
    import { useVisibilidadNavegador } from '../composables/useVisibilidadNavegador'
    import ContenedorPaginaPrincipal from '../components/ContenedorPaginaPrincipal.vue'

    const { showNavegador } = useVisibilidadNavegador()

</script>

<template>
    <div class="flex flex-col min-h-screen">
        <Cabecera />
        <div class="flex flex-row">
            <Navegador v-if="showNavegador" />
            <main :class="['flex-1 p-4', showNavegador ? 'ml-45' : 'ml-0']">
                <ContenedorPaginaPrincipal></ContenedorPaginaPrincipal>
            </main>
            
        </div>
    </div>

</template>

<style>

</style>