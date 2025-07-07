<script setup>
    import { updateOperacion, removeOperacion } from '../scripts/llamadasOperacion'
    import { ref, watch } from 'vue'

    const props = defineProps({
        operacion: {
            type: Object,
            required: true
        }
    })

    const emit = defineEmits(['operacion-updated', 'operacion-deleted'])
    const editModalOperacion = ref(null)

    const localOperacion = ref({
        id: 0,
        nombre: '',
        estado: '',
        fechaInicio: '',
        fechaFinal: ''
    })

    watch(() => props.operacion, (newValue) => {
        if(newValue && typeof newValue === 'object'){
            localOperacion.value = {
                id: newValue.id ?? 0,
                nombre: newValue.nombre || '',
                estado: newValue.estado || '',
                fechaInicio: newValue.fechaInicio || '',
                fechaFinal: newValue.fechaFinal || ''
            };
        } else {
            localOperacion.value = {
                id: 0,
                nombre: '',
                estado: '',
                fechaInicio: '',
                fechaFinal: ''
            };
        }
    }, {immediate: true})

    const openModal = () => {
        console.log('editModalOperacion:', editModalOperacion.value)
        if(editModalOperacion.value){
            editModalOperacion.value.classList.add('modal-open')
        } else {
            console.error('Modal no encontrado')
        }
    }

    const closeModal = () => {
        if(editModalOperacion.value){
            editModalOperacion.value.classList.remove('modal-open')
        }
    }

    const handleSaveOperacion = async () => {
        try{
            const success = await updateOperacion(localOperacion.value)
            if(success){
                emit('operacion-updated')
                document.getElementById('edit_modal_operacion').close()
            }
        }catch(error){
            console.error('Error en handleSaveOperacion: ', error)
        }
    }

    const handleDeleteOperacion = async () => {
        if(!confirm('¿Estás seguro de que quieres eliminar esta operación?')) return

        try{
            const success = await removeOperacion(props.operacion.id)
            if(success){
                emit('operacion-deleted')
                document.getElementById('edit_modal_operacion').close()
            }
        }catch(error){
            console.error('Error eliminando operación: ', error)
        }
    }
</script>

<template>

  <button class="btn btn-soft btn-info border border-gray-600" @click="openModal">···</button>
  <div id="edit_modal_operacion" class="modal" ref="editModalOperacion">
    <div class="modal-box">
      <h3 class="text-lg font-bold">Editar operacion</h3>
      <div class="flex flex-col">
        <form @submit.prevent="handleSaveOperacion">
          <div class="mb-4">
            <p>Nombre:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localOperacion.nombre"
              required
              placeholder="P.e: Operación A"
            />
          </div>
          <div class="mb-4">
            <p>Estado:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localOperacion.estado"
              required
              placeholder="P.e: Planificada, completada..."
            />
          </div>
          <div class="mb-4">
             <p>Fecha final:</p>
            <input
              class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
              v-model="localOperacion.fechaFinal"
              required
              placeholder="P.e: dd/mm/yyyy"
              pattern="\d{2}/\d{2}/\d{4}"
            />
          </div>
          <!-- Agregar campo oculto para fechaInicio -->
          <input
            type="hidden"
            v-model="localOperacion.fechaInicio"
          />
          <div class="flex flex-row gap-2">
            <button
              type="submit"
              class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition"
            >
              Guardar Cambios
            </button>
            <button
              type="button"
              class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg shadow transition"
              @click="handleDeleteOperacion"
            >
              Eliminar Operación
            </button>
          </div>
        </form>
      </div>
      <div class="modal-action">
        <button class="btn" @click="closeModal">Cancelar</button>
      </div>
    </div>
  </div>

</template>

<style scoped>

</style>