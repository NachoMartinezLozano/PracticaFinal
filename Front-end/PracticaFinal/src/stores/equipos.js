import { defineStore } from 'pinia'
import { ref } from 'vue'

const apiURL = 'https://localhost:7154/api/Equipo'
const operacionApiURL = 'https://localhost:7154/api/Operacion'

export const useEquiposStore = defineStore('equipos', {
  state: () => ({
    equipos: ref([]), // Lista de equipos
    operaciones: ref([]),
    newEquipo: ref({
      nombre: '',
      especialidad: '',
      operacionId: 0
    }) // Objeto para nuevo equipo
  }),

  actions: {

    async fetchOperaciones(){
      console.log('Enviando solicitud de GET a: ', operacionApiURL)
      try{
        const url = `${operacionApiURL}`
        const response = await fetch(url)
        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al obtener las operaciones para los equipos')
        const data = await response.json()
        console.log('Operaciones obtenidas para los equipos: ', data)
        this.operaciones = data
      }catch(error){
        console.error('Error fetching las operaciones para los equipos: ', error)
        this.operaciones = []
      }
    },

    async fetchEquipos() {
      console.log('Enviando solicitud de GET a: ', apiURL);
      try {
        const url = `${apiURL}`
        const response = await fetch(url);
        console.log('Respuesta recibida:', response.status, response.statusText);
        if (!response.ok) throw new Error('Error al obtener los Equipos');
        const data = await response.json();
        console.log('Equipos obtenidos:', data);
        this.equipos = data; // Actualiza el estado reactivo
      } catch (error) {
        console.error('Error fetching equipos:', error);
        this.equipos = [];
      }
    },

    async addEquipo() {
      console.log('Función addEquipo ejecutada, newEquipo:', this.newEquipo);
      if (!this.newEquipo.nombre.trim() || !this.newEquipo.especialidad.trim() || !this.newEquipo.operacionId) {
        console.log('No se añadió equipo: el input está vacío');
        return false;
      }

      const item = {
        nombre: this.newEquipo.nombre.trim(),
        especialidad: this.newEquipo.especialidad.trim(),
        operacionId: parseInt(this.newEquipo.operacionId) // Asegurar que operacionId sea un número
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
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Error al añadir el equipo: ${response.status} ${response.statusText} - ${errorText}`);
        }
        this.newEquipo = { nombre: '', especialidad: '', operacionId: 0 }; // Resetea el formulario
        await this.fetchEquipos(); // Refresca la lista de equipos
        return true;
      } catch (error) {
        console.error('Error añadiendo equipo:', error);
        return false;
      }
    },

    async updateEquipo(equipo) {
      const updatedEquipo = {
        id: equipo.id,
        nombre: equipo.nombre,
        especialidad: equipo.especialidad,
        operacionId: parseInt(equipo.operacionId) // Asegurar que operacionId sea un número
      };

      try {
        console.log('Enviando solicitud PUT a:', `${apiURL}/${equipo.id}`);
        const response = await fetch(`${apiURL}/${equipo.id}`, {
          method: 'PUT',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedEquipo)
        });
        console.log('La petición de PUT con datos: ', JSON.stringify(updatedEquipo));
        console.log('Respuesta recibida:', response.status, response.statusText);
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Error al actualizar el equipo: ${response.status} ${response.statusText} - ${errorText}`);
        }
        await this.fetchEquipos(); // Refresca la lista de equipos
        return true;
      } catch (error) {
        console.error('Error updating equipo:', error);
        return false;
      }
    },

    async removeEquipo(equipoId) {
      try {
        console.log('Enviando solicitud DELETE a:', `${apiURL}/${equipoId}`);
        const response = await fetch(`${apiURL}/${equipoId}`, {
          method: 'DELETE'
        });
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Error al eliminar el equipo: ${response.status} ${response.statusText} - ${errorText}`);
        }
        await this.fetchEquipos(); // Refresca la lista de equipos
        return true;
      } catch (error) {
        console.error('Error eliminando equipo:', error);
        return false;
      }
    }
  }
});