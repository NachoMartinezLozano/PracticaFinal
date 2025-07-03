<script setup>

    import { addOperacion } from '../scripts/llamadasOperacion.js'

</script>

<template>
    <button class="btn btn-soft btn-info border border-gray-600" onclick="my_modal_1.showModal()">Añadir</button>
        <dialog id="my_modal_1" class="modal">
            <div class="modal-box">
                <h3 class="text-lg font-bold">Introduce los datos de la nueva operación:</h3>
                    <div class="flex flex-col">
                        <form @submit.prevent="addOperacion">
                            <p>Nombre:</p>
                            <input 
                                class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                                v-model="newOperacion"
                                required
                                placeholder="P.e: Operación A"
                            />
                             <button class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition">
                                Añadir tarea
                            </button>
                        </form>
                    </div>
                <div class="modal-action">
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