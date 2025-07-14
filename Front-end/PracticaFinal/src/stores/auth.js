import { defineStore } from 'pinia';
import { ref } from 'vue';

const apiURL = 'https://localhost:7154/api/Auth';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        user: ref(null), // Usuario autenticado
        loginData: ref({
            username: '',
            password: ''
        }),
        registerData: ref({
            username: '',
            email: '',
            password: '',
            name: ''
        })
    }),
    
    actions: {
        async login(){
            console.log('Enviando solicitud de POST a: ', `${apiURL}/login`)
            console.log('Datos de inicio de sesión:', JSON.stringify(this.loginData))
            if(!this.loginData.username || !this.loginData.password.trim())
            {
                console.error('El nombre de usuario y la contraseña son obligatorios');
                return false;
            }

            try
            {

                const response = await fetch(`${apiURL}/login`, {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.loginData)
                });
                console.log('Respuesta recibida: ', response.status, response.statusText)
                if(!response.ok)
                {
                    const errorData = await response.text()
                    throw new Error(`Error en la solicitud: ${response.status} - ${errorData}`);
                }
                const data = await response.json()
                console.log('Datos de usuario autenticado: ', data)
                this.user = {
                    username: this.loginData.username,
                    token: data.token,
                    refreshToken: data.refreshToken
                };
                this.loginData = { username: '', password: '' }; // Limpiar datos de inicio de sesión
                return true;

            }catch(error)
            {
                console.error('Error al iniciar sesión: ', error)
                this.user = null
                return false;
            }
        },

        async register()
        {
            console.log('Enviando solicitud de POST a : ', `${apiURL}/register`)
            console.log('Datos de registro:', JSON.stringify(this.registerData))
            if(!this.registerData.username || !this.registerData.email || !this.registerData.password.trim() || !this.registerData.name.trim())
            {
                console.error('Todos los campos son obligatorios');
                return false;
            }

            try{

                const response = await fetch(`${apiURL}/register`, {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.registerData)
                });
                console.log('Respuesta recibida: ', response.status, response.statusText)
                if(!response.ok)
                {
                    const errorData = await response.text()
                    throw new Error(`Error en la solicitud: ${response.status} - ${errorData}`);
                }
                const data = await response.json()
                console.log('Datos de usuario registrado: ', data)
                this.registerData = { username: '', email: '', password: '', name: '' }; // Limpiar datos de registro
                return true;

            }catch(error)
            {
                console.error('Error al registrar usuario: ', error)
                return false;
            }
        },

        logout()
        {
            console.log('Cerrando sesión')
            this.user = null
            this.loginData = { username: '', password: '' }; // Limpiar datos de inicio de sesión
        }
    }
})