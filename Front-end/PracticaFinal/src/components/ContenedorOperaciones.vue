<script setup>

    import BotonAñadirOperacion from './BotonAñadirOperacion.vue';
    import BotonEditarEliminarOperacion from './BotonEditarEliminarOperaciones.vue'
    import { useOperacionesStore } from '../stores/operaciones'
    import { onMounted } from 'vue'

    const operacionesStore = useOperacionesStore()

    onMounted(() => {
        operacionesStore.fetchOperaciones();
    })

</script>

<template>

    <div class="flex flex-col">

        <div class="flex flex-col text-center py-4">
            <h1 class="font-bold text-xl">GESTIONA LAS OPERACIONES DE TU EMPRESA</h1>
            <p class="font-bold">Gestiona todas tus operaciones de trabajo de forma eficiente y sin complicaciones con nuestra plataforma</p>
        </div>

        <div class="flex flex-row justify-between pt-4">
            <h2 class="font-bold px-4">Tus Operaciones:</h2>
            <div class="px-4">
                <BotonAñadirOperacion></BotonAñadirOperacion>
            </div>
        </div>
        <!-- Tabla con todas las operaciones -->
        <div class="overflow-x-auto mx-auto">
            <table class="min-w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2 table-auto">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Estado</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Fecha Inicio</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Fecha Final</th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-300 uppercase tracking-wider">Más</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="operacion in operacionesStore.operaciones" :key="operacion.id">
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ operacion.id }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ operacion.nombre }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ operacion.estado }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ operacion.fechaInicio }}</td>
                        <td class="px-6 py-4 text-center whitespace-nowrap text-sm text-gray-900 bg-gray-200">{{ operacion.fechaFinal }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 bg-gray-200">
                            <BotonEditarEliminarOperacion :operacion="operacion"></BotonEditarEliminarOperacion>
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