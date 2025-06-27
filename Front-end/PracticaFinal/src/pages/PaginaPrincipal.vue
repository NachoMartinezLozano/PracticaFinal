<script setup>
    import { ref, onMounted, onUnmounted} from 'vue'
    import Cabecera from '../components/Cabecera.vue'
    import Navegador from '../components/Navegador.vue'

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
        <div class="flex flex-1">
            <Navegador v-if="showNavegador" />
        </div>
    </div>

</template>

<style>

</style>