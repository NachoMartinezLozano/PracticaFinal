<script setup>

    import BotonAñadirAgentes from './BotonAñadirAgentes.vue'
    import BotonEditarEliminarAgente from './BotonEditarEliminarAgente.vue'
    import { useAgentesStore } from '../stores/agentes'
    import { onMounted } from 'vue'

    const agentesStore = useAgentesStore()

    onMounted(() => {
        agentesStore.fetchAgentes();
    })

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todos los agentes de la organización. </h1>
            <BotonAñadirAgentes></BotonAñadirAgentes>
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
                    <tr v-for="agente in agentesStore.agentes" :key="agente.id">
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.id }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.nombre }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.rango }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.activo ? 'Si' : 'No' }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ agente.equipoId }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                             <BotonEditarEliminarAgente :agente="agente"></BotonEditarEliminarAgente> 
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>