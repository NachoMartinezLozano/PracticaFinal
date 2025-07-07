import { defineStore } from 'pinia'
import { ref } from 'vue'

const apiURL = 'https://localhost:7154/api/Operacion'

export const useOperacionesStore = defineStore('operaciones', {
    state: () => ({
        operaciones: ref([]),
        newOperacion: ref({
            nombre: '',
            fechaInicio: '',
            fechaFinal: ''
        })
    }),
    
    actions: {
        async fetchOperaciones(){
            console.log('Enviando solicitud de GET a: ', apiURL);
            try {
                const url = `${apiURL}`
                const response = await fetch(url);
                console.log('Respuesta recibida:', response.status, response.statusText);
                if (!response.ok) throw new Error('Error al obtener las Operaciones');
                const data = await response.json();
                console.log('Operaciones obtenidas:', data);
                this.operaciones = data; // Actualiza el estado reactivo
            } catch (error) {
                console.error('Error fetching operaciones:', error);
                this.operaciones = [];
            }
        },

        async addOperacion(){
            console.log('Función addOperacion ejecutada, newOperacion:', this.newOperacion);
            if (!this.newOperacion.nombre.trim() || !this.newOperacion.fechaInicio.trim() || !this.newOperacion.fechaFinal.trim()) {
                console.log('No se añadió operación: el input está vacío');
                return false;
            }

            const item = {
                nombre: this.newOperacion.nombre.trim(),
                estado: 'Planificada',
                fechaInicio: this.newOperacion.fechaInicio.trim(),
                fechaFinal: this.newOperacion.fechaFinal.trim()
            };

            try {
                console.log('Enviando solicitud POST a:', apiURL);
                console.log('Cuerpo de la solicitud:', JSON.stringify(item));
                const response = await fetch(apiURL, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
                });
                console.log('Respuesta recibida:', response.status, response.statusText);
                if (!response.ok) throw new Error(`Error al añadir la operación: ${response.status} ${response.statusText}`);
                this.newOperacion = { nombre: '', fechaInicio: '', fechaFinal: '' }; // Resetea el formulario
                await this.fetchOperaciones(); // Refresca la lista de operaciones
                return true;
            } catch (error) {
                console.error('Error añadiendo operación:', error);
                return false;
            }
        },

        async removeOperacion(operacionId){
            try {
                console.log('Enviando solicitud DELETE a:', `${apiURL}/${operacionId}`);
                const response = await fetch(`${apiURL}/${operacionId}`, {
                method: 'DELETE'
                });
                if (!response.ok) throw new Error('Error al eliminar la operación');
                await this.fetchOperaciones(); // Refresca la lista de operaciones
                return true;
            } catch (error) {
                console.error('Error eliminando operación:', error);
                return false;
            }
        },

        async updateOperacion(operacion){
            const updatedOperacion = {
            id: operacion.id,
            nombre: operacion.nombre,
            estado: operacion.estado,
            fechaInicio: operacion.fechaInicio,
            fechaFinal: operacion.fechaFinal
            };

            try {
                console.log('Enviando solicitud PUT a:', `${apiURL}/${operacion.id}`);
                const response = await fetch(`${apiURL}/${operacion.id}`, {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedOperacion)
                });
                console.log('La petición de PUT con datos: ', JSON.stringify(updatedOperacion));
                console.log('Respuesta recibida:', response.status, response.statusText);
                if (!response.ok) throw new Error(`Error al actualizar la operación: ${response.status} ${response.statusText}`);
                await this.fetchOperaciones(); // Refresca la lista de operaciones
                return true;
            } catch (error) {
                console.error('Error updating operación:', error);
                return false;
            }
        }
    }
})