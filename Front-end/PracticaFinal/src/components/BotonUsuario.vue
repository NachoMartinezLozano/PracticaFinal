<script setup>

    import { ref } from 'vue'
    import fotoPerfil from '../imagenes/foto-perfil.webp'
    import { useAuthStore } from '../stores/auth'

    const authStore = useAuthStore()
    const isOpen = ref(false)

</script>

<template>

    <div class="dropdown dropdown-end">
        <button @click="isOpen = !isOpen" class="btn btn-ghost btn-circle w-7 h-7">
            <img :src="fotoPerfil" alt="Perfil" class="w-7 h-7 rounded-full">
        </button>
        <ul
            tabindex="0"
            class="menu menu-sm dropdown-content bg-base-100 rounded-box z-1 mt-3 p-2 shadow">
            <li><button class="btn btn-soft btn-info">Modificar datos</button></li>
            <li><button class="btn btn-soft btn-error" @click="authStore.logout()">Log out</button></li>
        </ul>
    </div>

</template>

<style scoped>

</style>