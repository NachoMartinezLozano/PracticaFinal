<script setup>

    import { useEquiposStore } from '../stores/equipos'

    const equiposStore = useEquiposStore()

    const handleAddEquipo = async () => {
        const success = await equiposStore.addEquipo();
        if(success){
            document.getElementById('my_modal_2').close();
        }
    }

</script>

<template>
<button class="btn btn-soft btn-info border border-gray-600" onclick="my_modal_2.showModal()">Añadir</button>
    <dialog id="my_modal_2" class="modal">
        <div class="modal-box">
            <h3 class="text-lg font-bold">Introduce los datos del nuevo equipo:</h3>
            <div class="flex flex-col">
                <form id="añadir-equipo" @submit.prevent="handleAddEquipo">
                    <div class="mb-4">
                        <p>Nombre:</p>
                        <input
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="equiposStore.newEquipo.nombre"
                            required
                            placeholder="P.e: Equipo A"
                        />
                    </div>
                    <div class="mb-4">
                        <p>Especialidad:</p>
                        <input
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                            v-model="equiposStore.newEquipo.especialidad"
                            required
                            placeholder="P.e: IA, Ciberseguridad..."
                        />
                    </div>
                    <div class="mb-4">
                        <!-- De momento está con el Id de la operación, cuando establezca las relaciones entre las tablas haré para que se relacione con el nombre de la Operación -->
                        <p>Operación de trabajo:</p>
                        <select
                            class="px-4 py-2 rounded-lg bg-gray-700 border border-gray-600 text-white focus:outline-none focus:ring-2 focus:ring-blue-500 transition w-full"
                            v-model="equiposStore.newEquipo.operacionId"
                        >
                            <option value="0" disabled>Selecciona una operación</option>
                            <option v-for="operacion in equiposStore.operaciones" :key="operacion.id" :value="operacion.id">
                                {{ operacion.nombre }}
                            </option>
                        </select>
                    </div>
                </form>
            </div>
            <div class="modal-action flex flex-row justify-end gap-2 mt-4">
                   <button form="añadir-equipo" type="submit" class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg shadow transition">
                        Añadir equipo
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