<script setup>

    import BotonAñadirOperacion from './BotonAñadirOperacion.vue';

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todas las operaciones existentes dentro de la organización. </h1>
            <BotonAñadirOperacion></BotonAñadirOperacion>
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
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>