import { defineStore } from 'pinia';
import { ref } from 'vue';

const apiURL = 'https://localhost:7154/api/Agente';

export const useAgentesStore = defineStore('agentes', {
  state: () => ({
    agentes: ref([]), // Lista de agentes
    newAgente: ref({
      nombre: '',
      rango: '',
      activo: false,
      equipoId: 0
    }) // Objeto para nuevo agente
  }),

  actions: {
    async fetchAgentes() {
      console.log('Enviando solicitud de GET a: ', apiURL);
      try {
        const url = `${apiURL}`
        const response = await fetch(url);
        console.log('Respuesta recibida:', response.status, response.statusText);
        if (!response.ok) throw new Error('Error al obtener los Agentes');
        const data = await response.json();
        console.log('Agentes obtenidos:', data);
        this.agentes = data; // Actualiza el estado reactivo
      } catch (error) {
        console.error('Error fetching agentes:', error);
        this.agentes = [];
      }
    },

    async addAgente() {
      console.log('Función addAgente ejecutada, newAgente:', this.newAgente);
      if (!this.newAgente.nombre.trim() || !this.newAgente.rango.trim() || this.newAgente.equipoId === 0) {
        console.log('No se añadió agente: el input está vacío');
        return false;
      }

      const item = {
        nombre: this.newAgente.nombre.trim(),
        rango: this.newAgente.rango.trim(),
        activo: this.newAgente.activo, // Ya es booleano
        equipoId: parseInt(this.newAgente.equipoId) // Asegurar que equipoId sea un número
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
          throw new Error(`Error al añadir el agente: ${response.status} ${response.statusText} - ${errorText}`);
        }
        this.newAgente = { nombre: '', rango: '', activo: false, equipoId: 0 }; // Resetea el formulario
        await this.fetchAgentes(); // Refresca la lista de agentes
        return true;
      } catch (error) {
        console.error('Error añadiendo agente:', error);
        return false;
      }
    },

    async updateAgente(agente) {
      const updatedAgente = {
        id: agente.id,
        nombre: agente.nombre,
        rango: agente.rango,
        activo: agente.activo, // Ya es booleano
        equipoId: parseInt(agente.equipoId) // Asegurar que equipoId sea un número
      };

      try {
        console.log('Enviando solicitud PUT a:', `${apiURL}/${agente.id}`);
        const response = await fetch(`${apiURL}/${agente.id}`, {
          method: 'PUT',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedAgente)
        });
        console.log('La petición de PUT con datos: ', JSON.stringify(updatedAgente));
        console.log('Respuesta recibida:', response.status, response.statusText);
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Error al actualizar el agente: ${response.status} ${response.statusText} - ${errorText}`);
        }
        await this.fetchAgentes(); // Refresca la lista de agentes
        return true;
      } catch (error) {
        console.error('Error updating agente:', error);
        return false;
      }
    },

    async removeAgente(agenteId) {
      try {
        console.log('Enviando solicitud DELETE a:', `${apiURL}/${agenteId}`);
        const response = await fetch(`${apiURL}/${agenteId}`, {
          method: 'DELETE'
        });
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Error al eliminar el agente: ${response.status} ${response.statusText} - ${errorText}`);
        }
        await this.fetchAgentes(); // Refresca la lista de agentes
        return true;
      } catch (error) {
        console.error('Error eliminando agente:', error);
        return false;
      }
    }
  }
});