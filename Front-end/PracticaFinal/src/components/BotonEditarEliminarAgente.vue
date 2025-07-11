<script setup>

    import { useAgentesStore } from '../stores/agentes'
    import { ref, watch } from 'vue'

    const props = defineProps({
        agente: {
            type: Object,
            required: true
        }
    })

    const agentesStore = useAgentesStore()
    const editModal = ref(null)

    const localAgente = ref({
        id: 0,
        nombre: '',
        rango: '',
        activo: '',
        equipoId: 0
    })

    watch(() => props.agente, (newValue) => {
        if (newValue && typeof newValue === 'object') {
            localAgente.value = {
                id: newValue.id ?? 0,
                nombre: newValue.nombre || '',
                rango: newValue.rango || '',
                activo: newValue.activo ?? false,
                equipoId: newValue.equipoId ?? 0
            };
        } else {
            // Valores por defecto si agente es undefined
            localAgente.value = {
                id: 0,
                nombre: '',
                rango: '',
                activo: false,
                equipoId: 0
            };
        }
    }, { immediate: true })

    const openModal = () => {
        console.log('editModal:', editModal.value) // Depurar
        if (editModal.value) {
            editModal.value.classList.add('modal-open')
        } else {
            console.error('Modal no encontrado. Verifica que el elemento con ref="editModal" esté renderizado.')
        }
        }

        const closeModal = () => {
        if (editModal.value) {
            editModal.value.classList.remove('modal-open')
        }
        }

    const handleSaveAgente = async () => {
        try{
            const success = await agentesStore.updateAgente(localAgente.value)
            if(success){
                closeModal()
            }
        }catch(error){
            console.error('Error en handleSaveAgente: ', error)
        }
    }

    const handleDeleteAgente = async () => {
        if(!confirm('¿Estás seguro de que quieres eliminar este agente?')) return

        try{
            const success = await agentesStore.removeAgente(props.agente.id)
            if(success){
                closeModal()
            }
        }catch(error){
            console.error('Error eliminando agente: ', error)
        }
    }

</script>

<template>

    <button class="btn btn-soft btn-info border border-gray-600" @click="openModal">···</button>
  <div id="edit_modal" class="modal" ref="editModal">
    <div class="modal-box">
      <h3 class="text-lg font-bold">Editar agente</h3>
      <div class="flex flex-col">
        <form id="editar-agente" @submit.prevent="handleSaveAgente">
          <div class="mb-4">
            <p>Nombre:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localAgente.nombre"
              required
              placeholder="P.e: Pepito Díaz"
            />
          </div>
          <div class="mb-4">
            <p>Rango:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localAgente.rango"
              required
              placeholder="P.e: Empleado, Gerente ..."
            />
          </div>
          <div class="mb-4">
            <select
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localAgente.activo"
              required
            >
              <option value="" disabled>Selecciona una opción</option>
              <option value="true">Sí</option>
              <option value="false">No</option>
            </select>
          </div>
          <div class="mb-4">
            <p>Equipo ID:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localAgente.equipoId"
              required
              placeholder="P.e: 1, 2..."
            />
          </div>

        </form>
      </div>
      <div class="modal-action flex flex-row gap-2 mt-4">
         <button
              form="editar-agente"
              type="submit"
              class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition"
            >
              Guardar Cambios
            </button>
            <button
              type="button"
              class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg shadow transition"
              @click="handleDeleteAgente"
            >
              Eliminar Agente
            </button>
        <button class="btn" @click="closeModal">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>