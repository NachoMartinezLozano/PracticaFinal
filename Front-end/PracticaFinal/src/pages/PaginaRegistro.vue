<script setup>

    import { useAuthStore } from '../stores/auth'
    import { useRouter } from 'vue-router'

    const router = useRouter()
    const authStore = useAuthStore()

    const handleRegister = async () => {
        const success = await authStore.register()
        if(success){
            console.log('Registration successful')
            router.push('/login')
        } else {
            console.error('Registration failed')
            // Aquí podrías mostrar un mensaje de error al usuario
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
    </div>
</template>

<style scoped>

</style>