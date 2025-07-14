<script setup>
    import { ref, onMounted, onUnmounted } from 'vue'
    import { useRouter } from 'vue-router'
    import { useVisibilidadNavegador } from '../composables/useVisibilidadNavegador'
    import { useAuthStore } from '../stores/auth'
    import BotonUsuario from './BotonUsuario.vue'

    const router = useRouter()
    const nombreEmpresa = 'CyberPulse Labs'
    const authStore = useAuthStore()

    const { showNavegador, toggleNavegador }  = useVisibilidadNavegador()

     // Variable para controlar el cambio de tema
    const theme = ref(localStorage.getItem('theme') || (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'))

    // Aplicar el tema al cargar la página
    const applyTheme = () => {
        document.documentElement.setAttribute('data-theme', theme.value)
        localStorage.setItem('theme', theme.value)
    }
    
    // Cambiar el tema al hacer clic sobre el toggle de cambio de modo
    const toggleTheme = () => {
        theme.value = theme.value === 'light' ? 'dark' : 'light'
        applyTheme()
    }

    // Escuchar cambios en la preferencia del sistema
    const handleSystemThemeChange = (e) => {
        if (!localStorage.getItem('theme')) {
            theme.value = e.matches ? 'dark' : 'light'
            applyTheme()
        }
    }

    // Inicializar el tema y configurar el listener
    onMounted(() => {
        applyTheme()
        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', handleSystemThemeChange)
    })

    // Limpiar el listener al desmontar el componente
    onUnmounted(() => {
        window.matchMedia('(prefers-color-scheme: dark)').removeEventListener('change', handleSystemThemeChange)
    })

</script>

<template>

    <div class="navbar bg-base-100 flex w-full mx-auto">
        <!--
        <div class="flex-none">
            <button class="btn btn-ghost btn-circle" @click="toggleNavegador" aria-label="Toggle navigation menu">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h7" />
                </svg>
            </button>
        </div>
        -->
         <div class="dropdown">
            <div tabindex="0" role="button" class="btn btn-ghost btn-circle">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"> <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h7" /> </svg>
            </div>
            <ul
                tabindex="0"
                class="menu menu-sm dropdown-content bg-base-100 rounded-box z-1 mt-3 w-52 p-2 shadow">
                <li><button class="btn" @click="router.push({ path: '/operaciones'})">Operaciones</button></li>
                <li><button class="btn" @click="router.push({ path: '/equipos'})">Equipos</button></li>
                <li><button class="btn" @click="router.push({ path: '/agentes'})">Agentes</button></li>
            </ul>
        </div>
        <div class="flex-1 px-5">
            <button class="btn btn-soft btn-info" @click="router.push({ path: '/'})">{{ nombreEmpresa }}</button>
        </div>
        <div class="flex">
            <ul class="menu menu-horizontal px-1">
                
                <li>
                    <label class="swap swap-rotate">
                    <!-- this hidden checkbox controls the state -->
                    <input type="checkbox" :checked="theme === 'light'" @change="toggleTheme" />

                    <!-- sun icon -->
                    <svg
                        class="swap-on h-5 w-5 fill-current"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24">
                        <path
                        d="M5.64,17l-.71.71a1,1,0,0,0,0,1.41,1,1,0,0,0,1.41,0l.71-.71A1,1,0,0,0,5.64,17ZM5,12a1,1,0,0,0-1-1H3a1,1,0,0,0,0,2H4A1,1,0,0,0,5,12Zm7-7a1,1,0,0,0,1-1V3a1,1,0,0,0-2,0V4A1,1,0,0,0,12,5ZM5.64,7.05a1,1,0,0,0,.7.29,1,1,0,0,0,.71-.29,1,1,0,0,0,0-1.41l-.71-.71A1,1,0,0,0,4.93,6.34Zm12,.29a1,1,0,0,0,.7-.29l.71-.71a1,1,0,1,0-1.41-1.41L17,5.64a1,1,0,0,0,0,1.41A1,1,0,0,0,17.66,7.34ZM21,11H20a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Zm-9,8a1,1,0,0,0-1,1v1a1,1,0,0,0,2,0V20A1,1,0,0,0,12,19ZM18.36,17A1,1,0,0,0,17,18.36l.71.71a1,1,0,0,0,1.41,0,1,1,0,0,0,0-1.41ZM12,6.5A5.5,5.5,0,1,0,17.5,12,5.51,5.51,0,0,0,12,6.5Zm0,9A3.5,3.5,0,1,1,15.5,12,3.5,3.5,0,0,1,12,15.5Z" />
                    </svg>

                    <!-- moon icon -->
                    <svg
                        class="swap-off h-5 w-5 fill-current"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24">
                        <path
                        d="M21.64,13a1,1,0,0,0-1.05-.14,8.05,8.05,0,0,1-3.37.73A8.15,8.15,0,0,1,9.08,5.49a8.59,8.59,0,0,1,.25-2A1,1,0,0,0,8,2.36,10.14,10.14,0,1,0,22,14.05,1,1,0,0,0,21.64,13Zm-9.5,6.69A8.14,8.14,0,0,1,7.08,5.22v.27A10.15,10.15,0,0,0,17.22,15.63a9.79,9.79,0,0,0,2.1-.22A8.11,8.11,0,0,1,12.14,19.73Z" />
                    </svg>
                    </label>
                </li>
                <li>
                    <span v-if="authStore.user" class="text-sm font-medium mr-4">
                        Bienvenido/a, {{ authStore.user.username }}
                    </span>
                </li>
                <li>
                    <div v-if="authStore.user">
                        <BotonUsuario></BotonUsuario>
                    </div>
                </li>
            </ul>
        </div>
    </div>

</template>

<style scoped>

    .navbar{
        z-index: 50;
    }

</style>