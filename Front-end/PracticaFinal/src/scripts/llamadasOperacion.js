import { ref, onMounted, onUnmounted } from 'vue'

const apiURL = 'https://localhost:7154/api/Operacion'
let id = 0
export const newOperacion = ref({
    nombre: '',
    fechaInicio:'',
    fechaFinal:''    
})
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

export async function addOperacion(operacion){
    console.log('Función addOperacion ejecutada, newOperacion:', operacion)
    if(!operacion.nombre.trim() || !operacion.fechaInicio.trim() || !operacion.fechaFinal.trim()){
        console.log('No se añadión operación: el input está vacío')
        return false;
    }

    const item = {
        nombre: operacion.nombre.trim(),
        estado: 'Planificada',
        fechaInicio: operacion.fechaInicio.trim(),
        fechaFinal: operacion.fechaFinal.trim()
    }

    try{
        console.log('Enviando solicitud POST a:', apiURL)
        console.log('Cuerpo de la solicitud:', JSON.stringify(item))
        const response = await fetch(apiURL, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        console.log('Respuesta recibida:', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al añadir la operación: ${response.status} ${response.statusText} - ${errorText}')
        newOperacion.value = {nombre:'', fechaInicio: '', fechaFinal:''}
        return true
    }catch(error){
        console.error('Error añadiendo operacion:', error)
        return false
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