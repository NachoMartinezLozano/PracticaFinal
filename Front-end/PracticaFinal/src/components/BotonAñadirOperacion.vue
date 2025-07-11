<script setup>

    import { useOperacionesStore } from '../stores/operaciones'

    const operacionesStore = useOperacionesStore()

    const handleAddOperacion = async () => {
        const success = await operacionesStore.addOperacion();
        if(success){
            document.getElementById('my_modal_1').close();
        }
    }

</script>

<template>
    <button class="btn btn-soft btn-info border border-gray-600" onclick="my_modal_1.showModal()">Añadir</button>
        <dialog id="my_modal_1" class="modal">
            <div class="modal-box">
                <h3 class="text-lg font-bold">Introduce los datos de la nueva operación:</h3>
                    <div class="flex flex-col">
                        <form id="añadir-operacion" @submit.prevent="handleAddOperacion">
                             <div class="mb-4">
                                <p>Nombre:</p>
                                <input 
                                    class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                                    v-model="operacionesStore.newOperacion.nombre"
                                    required
                                    placeholder="P.e: Operación A"
                                />
                            </div>
                            <div class="mb-4">
                                <p>Fecha inicio:</p>
                                <input 
                                    class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                                    v-model="operacionesStore.newOperacion.fechaInicio"
                                    required
                                    placeholder="DD/MM/YYYY"
                                    pattern="\d{2}/\d{2}/\d{4}"
                                    title="Formato: DD/MM/YYYY"
                                />
                            </div>
                            <div class="mb-4">
                                <p>Fecha final:</p>
                                <input 
                                    class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                                    v-model="operacionesStore.newOperacion.fechaFinal"
                                    required
                                    placeholder="DD/MM/YYYY"
                                    pattern="\d{2}/\d{2}/\d{4}"
                                    title="Formato: DD/MM/YYYY"
                                />
                            </div>
                        </form>
                    </div>
                <div class="modal-action flex flex-row justify-end gap-2 mt-4">
                    <button form="añadir-operacion" type="submit" class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition">
                        Añadir operación
                    </button>
                    <form method="dialog">
                        <!-- if there is a button in form, it will close the modal -->
                        <button class="btn">Cancelar</button>
                    </form>
                </div>
            </div>
        </dialog>
</template>

<style scoped>

</style>