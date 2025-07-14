<script setup>

    import { useAuthStore} from '../stores/auth'
    import { useRouter } from 'vue-router'
    import { ref } from 'vue'
    const router = useRouter()
    const authStore = useAuthStore()
    const showSuccess = ref(false)
    const showError = ref(false)

    const handleLogin = async () => {
        const success = await authStore.login()
        if(success){
            console.log('Login successful')
            showSuccess.value = true
            setTimeout(() => {
                showSuccess.value = false;
            }, 3000);
        }else{
            console.error('Login failed')
            showError.value = true
             setTimeout(() => {
                showError.value = false;
            }, 3000);
        }
    
    }

</script>
<template>

    <div class="card bg-base-100">
        <div class="card-body">
            <div class="flex flex-col items-center">
            <h2 class="text-xl font-bold">Introduzca sus credenciales.</h2>
            </div>
            <div class="flex flex-col items-center py-2">
                <p class="font-bold mb-2">Nombre de usuario</p>
                <input v-model="authStore.loginData.username" type="text" placeholder="Nombre de usuario" class="input input-bordered mb-2" />
                <p class="font-bold mb-2">Contraseña</p>
                <input v-model="authStore.loginData.password" type="password" placeholder="Contraseña" class="input input-bordered mb-2" />
                <button 
                    @click="handleLogin" 
                    class="btn btn-info mt-2"
                >
                    Iniciar Sesión
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
            <div class="flex flex-col items-center">
                <p class="font-bold">¿Aún no tiene cuenta?</p>
                <p class="font-bold">Regístrese pinchado <a class="text-blue-600 hover:underline" @click.prevent="router.push('/registro')">Aquí</a></p>
            </div>
        </div>
    </div>

</template>

<style scoped>

</style>