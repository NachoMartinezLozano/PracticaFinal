import { ref, onMounted, onUnmounted } from 'vue'

const apiURL = 'https://localhost:7154/api/Operacion'
let id = 0
const newOperacion = ref('')
const editingOperacionId = ref(null)
const editedOperacionName = ref('')

onMounted(() => {
    fetchOperaciones()
})

export async function fetchOperaciones(){
    console.log('Enviando solicitud de GET a: ', apiURL)
    try{
        const url = `${apiURL}`
        const response = await fetch(url)

        console.log('Respuesta recibida:', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al obtener las Operaciones')
        const data = await response.json()
        console.log('Operaciones obtenidas:', data)
        return data
    } catch(error){
        console.error('Error fetching operaciones:', error)
        return []
    }
}

export async function addOperacion(){
    console.log('Función addOperacion ejecutada, newOperacion:', newOperacion.value)
    if(!newOperacion.value.trim()){
        console.log('No se añadión equipo: el input está vacío')
        return;
    }

    const item = {
        nombre: newOperacion.value.trim(),
        estado: 'planificada',
        fechaInicio: new Date().toISOString(),
        fechaFin: new Date().toISOString()
    }

    try{
        console.log('Enviando solicitud POST a:', apiURL)
        const response = await fetch(apiURL, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        console.log('Respuesta recibida:', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al añadir la operación')
        newOperacion.value = ''
    }catch(error){
        console.error('Error añadiendo operacion:', error)
    }
}

export async function removeOperacion(operacionId){
    try{
        console.log('Enviando solicitud DELETE a:', `${apiURL}/${operacionId}`)
        const response = await fetch(`${apiURL}/${operacionId}`, {
            method: 'DELETE'
        })

        if(!response.ok) throw new Error('Error al eliminar la operación')
    }catch(error){
        console.error('Error eliminando la operación:', error)
    }
}

export async function updateOperacion(operacion){
    const updatedOperacion = {
        id: operacion.id,
        nombre: operacion.nombre,
        estado: operacion.estado,
        fechaInicio: operacion.fechaInicio,
        fechaFin: operacion.fechaFin
    }

    try{
        console.log('Enviando solicitud PUT a:', `${apiURL}/${operacion.id}`)
        const response = await fetch(`${apiURL}/${operacion.id}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedOperacion)
        })
        console.log('Respuesta recibida:', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al actualizar la operación')
        editingOperacionId = null
        editedOperacionName = ''
    }catch(error){
        console.error('Error updating operación:', error)
    }
}