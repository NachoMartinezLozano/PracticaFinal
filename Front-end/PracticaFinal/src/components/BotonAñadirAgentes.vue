<script setup>

    import { useAgentesStore } from '../stores/agentes'

    const agentesStore = useAgentesStore()

    const handleAddAgente = async () => {
        const success = await agentesStore.addAgente();
        if(success){
            document.getElementById('my_modal_3').close();
        }
    }

</script>

<template>
    <button class="btn btn-soft btn-info border border-gray-600" onclick="my_modal_3.showModal()">Añadir</button>
    <dialog id="my_modal_3" class="modal">
        <div class="modal-box">
            <h3 class="text-lg font-bold">Introduce los datos del nuevo agente:</h3>
            <div class="flex flex-col">
                <form id="añadir-agente" @submit.prevent="handleAddAgente">
                    <div class="mb-4">
                        <p>Nombre:</p>
                        <input 
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="agentesStore.newAgente.nombre"
                            required
                            placeholder="P.e: Pepito Díaz"
                        />
                    </div>
                    <div class="mb-4">
                        <p>Rango:</p>
                        <input 
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="agentesStore.newAgente.rango"
                            required
                            placeholder="P.e: Empleado, Gerente..."
                        />
                    </div>
                    <div class="mb-4">
                        <p>Activo:</p>
                        <select 
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="agentesStore.newAgente.activo"
                            required
                        >
                            <option value="" disabled>Selecciona una opción</option>
                            <option :value="true">Sí</option>
                            <option :value="false">No</option>
                        </select>
                    </div>
                    <div class="mb-4">
                        <p>Equipo de trabajo:</p>
                        <input 
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="agentesStore.newAgente.equipoId"
                            required
                            placeholder="P.e: 1,2..."
                        />
                    </div>
                </form>
            </div>
            <div class="modal-action flex flex-row gap-2 mt-4">
                <button form="añadir-agente" type="submit" class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition">
                    Añadir agente
                </button>
                <form method="dialog">
                    <button class="btn">Cancelar</button>
                </form>
            </div>
        </div>
    </dialog>
</template>

<style scoped>

</style>