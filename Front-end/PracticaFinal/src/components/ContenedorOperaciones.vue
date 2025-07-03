<script setup>

    import BotonAñadirOperacion from './BotonAñadirOperacion.vue';
    import { fetchOperaciones } from '../scripts/llamadasOperacion'
    import { ref, onMounted } from 'vue'

    const operaciones = ref([])
    
    const loadOperaciones = async () => {
        try{
            const data = await fetchOperaciones();
            operaciones.value = data
        }catch(err){
            console.error('Error cargando operaciones en contenedor:', err)
            operaciones.value = []
        }
    }

    const handleOperacionAdded = () => {
        loadOperaciones();
    }

    onMounted(loadOperaciones)

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todas las operaciones existentes dentro de la organización. </h1>
            <BotonAñadirOperacion @operacion-added="handleOperacionAdded"></BotonAñadirOperacion>
        </div>
        <!-- Tabla con todas las operaciones -->
        <div >
            <table class="min-w-full bg-white border border-gray-700 rounded-lg shadow-md mt-2">
                <thead class="bg-base-100">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">ID</th>
                        <th class="px-8 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Nombre</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Estado</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Fecha Inicio</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-300 uppercase tracking-wider">Fecha Final</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200">
                    <tr v-for="operacion in operaciones" :key="operacion.id">
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ operacion.id }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ operacion.nombre }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ operacion.estado }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ operacion.fechaInicio }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ operacion.fechaFinal }}</td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>