<script setup>

    import BotonAñadirEquipos from './BotonAñadirEquipos.vue';
    import BotonEditarEliminarEquipos from './BotonEditarEliminarEquipos.vue';
    import { useEquiposStore } from '../stores/equipos'
    import { onMounted } from 'vue'
    import { useAuthStore } from '../stores/auth'

    const equiposStore = useEquiposStore()
    const authStore = useAuthStore()

    onMounted(() => {
        equiposStore.fetchEquipos();
        equiposStore.fetchOperaciones();
    })

</script>

<template>

    <div class="flex flex-col">

        <div class="flex flex-col text-center py-4">
            <h1 class="font-bold text-xl">GESTIONA LOS EQUIPOS DE TU EMPRESA</h1>
            <p class="font-bold">Gestiona todas tus equipos de trabajo de forma eficiente y sin complicaciones con nuestra plataforma</p>
        </div>

        <div class="flex flex-row justify-between pt-4">
            <h1 class="font-bold px-4">Tus equipos: </h1>
            <div v-if="authStore.user" class="px-4">
                <BotonAñadirEquipos></BotonAñadirEquipos>
            </div>
            
        </div>
        <!-- Tabla con todas las operaciones -->
        <div class="overflow-x-auto mx-auto">
            <table class="min-w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Especialidad</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Operacion de Trabajo</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Más</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="equipo in equiposStore.equipos" :key="equipo.id">
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ equipo.id }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ equipo.nombre }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ equipo.especialidad }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ equipo.operacionNombre || 'Sin operación' }}</td>
                        <td v-if="authStore.user" class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 bg-gray-200">
                            <BotonEditarEliminarEquipos :equipo="equipo"></BotonEditarEliminarEquipos>
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
    direction: ltr;
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