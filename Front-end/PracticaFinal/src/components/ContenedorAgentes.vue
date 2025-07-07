<script setup>

    import BotonAñadirAgentes from './BotonAñadirAgentes.vue'
    import BotonEditarEliminarAgente from './BotonEditarEliminarAgente.vue'
    import { fetchAgentes } from '../scripts/llamadasAgentes'
    import { ref, onMounted } from 'vue'

    const agentes = ref([])

    const loadAgentes = async () => {
        try{
            const data = await fetchAgentes()
            agentes.value = data
        }catch(err){
            console.error('Error cargando agentes en contenedor: ', err)
            agentes.value = []
        }
    }

    const handleAgenteAdded = () => {
        loadAgentes(); // Recargar la lista cuando se añade un agente
    }

    const handleAgenteUpdated = () => {
    loadAgentes(); // Recargar la lista cuando se actualiza un agente
    };

    const handleAgenteDeleted = () => {
    loadAgentes(); // Recargar la lista cuando se elimina un agente
    };

    onMounted(loadAgentes)

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todos los agentes de la organización. </h1>
            <BotonAñadirAgentes @agente-added="handleAgenteAdded"></BotonAñadirAgentes>
        </div>
        <!-- Tabla con todas las operaciones -->
        <div >
            <table class="w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Rango</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Activo</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Equipo_ID</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wide"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="agente in agentes" :key="agente.id">
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.id }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.nombre }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.rango }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.activo ? 'Si' : 'No' }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.equipoId }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                             <BotonEditarEliminarAgente :agente="agente" @agente-updated="handleAgenteUpdated" @agente-deleted="handleAgenteDeleted"></BotonEditarEliminarAgente> 
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>