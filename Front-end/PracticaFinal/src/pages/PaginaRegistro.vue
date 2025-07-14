<script setup>

    import { useAuthStore } from '../stores/auth'
    import { useRouter } from 'vue-router'
    import { ref } from 'vue'

    const router = useRouter()
    const authStore = useAuthStore()
    const showSuccess = ref(false)
    const showError = ref(false)

    const handleRegister = async () => {
        const success = await authStore.register()
        if(success){
            console.log('Registration successful')
            showSuccess.value = true
            setTimeout(() => {
                showSuccess.value = false;
                console.log('Redirigiendo a Pagina Principal');
            }, 3000);
            router.push('/')
        } else {
            console.error('Registration failed')
            showError.value = true
            setTimeout(() => {
                showError.value = false;
            }, 3000);
        }
    }

</script>

<template>
    <div class="flex flex-col items-center py-2">
        <h2 class="text-2xl font-bold mb-4">Introduzca sus datos:</h2>
        <p class="font-bold mb-2">Nombre de Usuario</p>
        <input v-model="authStore.registerData.username" type="text" placeholder="Nombre de Usuario" class="input input-bordered mb-2" />
        <p class="font-bold mb-2">Nombre</p>
        <input v-model="authStore.registerData.name" type="text" placeholder="Nombre" class="input input-bordered mb-2" />
        <p class="font-bold mb-2">Correo</p>
        <input v-model="authStore.registerData.email" type="text" placeholder="Email" class="input input-bordered mb-2" />
        <p class="font-bold mb-2">Contraseña</p>
        <input v-model="authStore.registerData.password" type="password" placeholder="Contraseña" class="input input-bordered mb-2" />

         <button
            @click="handleRegister"
            class="btn btn-info text-white px-4 py-2 rounded-lg shadow transition"
        >
            Registrarme
        </button>
        <div v-if="showSuccess" class="badge badge-success mt-2">
            <svg class="size-[1em]" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                <g fill="currentColor" stroke-linejoin="miter" stroke-linecap="butt">
                    <circle cx="12" cy="12" r="10" fill="none" stroke="currentColor" stroke-linecap="square" stroke-miterlimit="10" stroke-width="2"></circle>
                    <polyline points="7 13 10 16 17 8" fill="none" stroke="currentColor" stroke-linecap="square" stroke-miterlimit="10" stroke-width="2"></polyline>
                </g>
            </svg>
            Success
        </div>
        <div v-if="showError" class="badge badge-error mt-2">
            <svg class="size-[1em]" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><g fill="currentColor"><rect x="1.972" y="11" width="20.056" height="2" transform="translate(-4.971 12) rotate(-45)" fill="currentColor" stroke-width="0"></rect><path d="m12,23c-6.065,0-11-4.935-11-11S5.935,1,12,1s11,4.935,11,11-4.935,11-11,11Zm0-20C7.038,3,3,7.037,3,12s4.038,9,9,9,9-4.037,9-9S16.962,3,12,3Z" stroke-width="0" fill="currentColor"></path></g></svg>
            Error
        </div>
    </div>
</template>

<style scoped>

</style>