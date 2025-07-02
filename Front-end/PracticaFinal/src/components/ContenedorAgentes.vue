<script setup>

</script>

<template>

    <div class="flex flex-col">
        <div class="flex flex-row justify-between">
            <h1>Gestiona todos los agentes de la organización. </h1>
            <button class="btn">Añadir</button>
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
                    </tr>
                </thead>
            </table>

        </div>
    </div>
</template>

<style scoped>

</style>