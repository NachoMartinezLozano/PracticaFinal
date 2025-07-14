<script setup>

    import { useEquiposStore } from '../stores/equipos'
    import { ref, watch } from 'vue'

    const props = defineProps({
        equipo: {
            type: Object,
            required: true
        }
    })

    const equiposStore = useEquiposStore()
    const editModalEquipo = ref(null)

    const localEquipo = ref({
        id: 0,
        nombre: '',
        especialidad: '',
        operacionId: 0
    })

    watch(() => props.equipo, (newValue) => {
        if(newValue && typeof newValue === 'object'){
            localEquipo.value = {
                id: newValue.id ?? 0,
                nombre: newValue.nombre || '',
                especialidad: newValue.especialidad || '',
                operacionId: newValue.operacionId ?? 0
            };
        } else {
            localEquipo.value = {
                id: 0,
                nombre: '',
                especialidad: '',
                operacionId: 0
            };
        }
    }, {immediate: true})

    const openModal = () => {
        if(editModalEquipo.value){
            editModalEquipo.value.classList.add('modal-open')
        } else{
            console.error('Modal no encontrado')
        }
    }

    const closeModal = () => {
        if(editModalEquipo.value){
            editModalEquipo.value.classList.remove('modal-open')
        }
    }

    const handleSaveEquipo = async () => {
        try{
            const success = await equiposStore.updateEquipo(localEquipo.value)
            if(success){
                closeModal()
            }
        }catch(error){
            console.error('Error en handleSaveEquipo: ', error)
        }
    }

    const handleDeleteEquipo = async () => {
        if(!confirm('¿Estás seguro de que quieres eliminar este equipo?')) return

        try{
            const success = await equiposStore.removeEquipo(props.equipo.id)
            if(success){
                closeModal()
            }
        }catch(error){
            console.error('Error eliminando equipo: ', error)
        }
    }

</script>

<template>

    <button class="btn btn-soft btn-info border border-gray-600" @click="openModal">···</button>
    <div id="edit_modal_equipo" class="modal" ref="editModalEquipo">
        <div class="modal-box">
        <h3 class="text-lg font-bold">Editar equipo</h3>
        <div class="flex flex-col">
            <form id="editar-equipo" @submit.prevent="handleSaveEquipo">
            <div class="mb-4">
                <p>Nombre:</p>
                <input
                class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                v-model="localEquipo.nombre"
                required
                placeholder="P.e: Equipo A"
                />
            </div>
            <div class="mb-4">
                <p>Especialidad:</p>
                <input
                class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                v-model="localEquipo.especialidad"
                required
                placeholder="P.e: IA, ciberseguridad..."
                />
            </div>
            <div class="mb-4">
                <p>Operación:</p>
                <select
                    class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white focus:outline-none focus:ring-2 focus:ring-blue-500 transition w-full"
                    v-model="localEquipo.operacionId"
                >
                    <option value="0" disabled>Selecciona una operación</option>
                    <option v-for="operacion in equiposStore.operaciones" :key="operacion.id" :value="operacion.id">
                        {{ operacion.nombre }}
                    </option>
                </select>
            </div>

            </form>
        </div>
        <div class="modal-action flex flex-row gap-2 mt-4">
            <button
                form="editar-equipo"
                type="submit"
                class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition"
                >
                Guardar Cambios
                </button>
                <button
                type="button"
                class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg shadow transition"
                @click="handleDeleteEquipo"
                >
                Eliminar Equipo
                </button>
            <button class="btn" @click="closeModal">Cancelar</button>
        </div>
        </div>
  </div>

</template>

<style scoped>

</style>