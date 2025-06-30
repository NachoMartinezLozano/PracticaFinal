<script setup>
    import { ref, onMounted, onUnmounted} from 'vue'
    import Cabecera from '../components/Cabecera.vue'
    import Navegador from '../components/Navegador.vue'
    import ContenedorPaginaPrincipal from '../components/ContenedorPaginaPrincipal.vue'

    const showNavegador = ref(window.innerWidth >= 768)

    const updateNavegadorVisibility = () => {
        showNavegador.value = window.innerWidth >= 768
    }

    onMounted(() => {
        window.addEventListener('resize', updateNavegadorVisibility)
    })

    onUnmounted(() => {
        window.removeEventListener('resize', updateNavegadorVisibility)
    })
</script>

<template>
    <div class="flex flex-col min-h-screen">
        <Cabecera />
        <div class="flex flex-row">
            <Navegador v-if="showNavegador" />
            <main :class="['flex-1 p-4', showNavegador ? 'ml-45' : 'ml-0']">
                <h1>Bienvenido a tu gestor DataStorm!</h1>
            </main>
            
        </div>
    </div>

</template>

<style>

</style>