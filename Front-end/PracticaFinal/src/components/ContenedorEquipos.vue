<script setup>

    import BotonAñadirEquipos from './BotonAñadirEquipos.vue';
    import BotonEditarEliminarEquipos from './BotonEditarEliminarEquipos.vue';
    import { useEquiposStore } from '../stores/equipos'
    import { onMounted } from 'vue'

    const equiposStore = useEquiposStore()

    onMounted(() => {
        equiposStore.fetchEquipos();
    })

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todos los equipos existentes dentro de la organización. </h1>
            <BotonAñadirEquipos></BotonAñadirEquipos>
        </div>
        <!-- Tabla con todas las operaciones -->
        <div >
            <table class="min-w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Especialidad</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Operacion_ID</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="equipo in equiposStore.equipos" :key="equipo.id">
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ equipo.id }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ equipo.nombre }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ equipo.especialidad }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ equipo.operacionId }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                            <BotonEditarEliminarEquipos :equipo="equipo"></BotonEditarEliminarEquipos>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>