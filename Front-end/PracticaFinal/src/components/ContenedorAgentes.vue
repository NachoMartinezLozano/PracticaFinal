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

        <div class="flex flex-col text-center py-4">
            <h1 class="font-bold text-xl">GESTIONA LOS AGENTES DE TU EMPRESA</h1>
            <p class="font-bold">Gestiona todos los agentes de tu empresa de forma eficiente y sin complicaciones con nuestra plataforma</p>
        </div>

        <div class="flex flex-row justify-between pt-4">
            <h1 class="px-4">Tus Agentes: </h1>
            <div class="px-4">
                <BotonAñadirAgentes></BotonAñadirAgentes>
            </div>
        </div>
        <!-- Tabla con todas las operaciones -->
        <div class="overflow-x-auto mx-auto">
            <table class="w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Rango</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Activo</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Equipo_ID</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wide"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="agente in agentesStore.agentes" :key="agente.id">
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ agente.id }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ agente.nombre }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ agente.rango }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ agente.activo ? 'Si' : 'No' }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ agente.equipoNombre || 'Sin equipo' }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 bg-gray-200">
                             <BotonEditarEliminarAgente :agente="agente"></BotonEditarEliminarAgente> 
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<style scoped>

        .overflow-x-auto {
        max-width: 100%;
        overflow-x: auto;
    }

    table {
    width: 100%;
    max-width: 900px; /* Limita el ancho máximo de la tabla */
    min-width: 600px; /* Asegura un ancho mínimo para evitar compresión excesiva */
    table-layout: fixed; /* Fija el ancho de las columnas según los valores de 'w-*' */
    }

    /* Mejora visual para pantallas pequeñas */
    @media (max-width: 640px) {
    table {
        font-size: 0.875rem; /* Reduce ligeramente el tamaño de la fuente en móviles */
    }
    
    th,
    td {
        padding: 0.5rem; /* Reduce el padding en pantallas pequeñas */
    }
}

</style>