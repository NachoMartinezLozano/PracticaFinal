import { ref, onMounted } from 'vue'

const apiURL = 'https://localhost:7154/api/Agente'
let id = 0
export const newAgente = ref({
    nombre: '',
    rango: '',
    activo: '',
    equipoId: 0
})
const editingAgenteId = ref(null)
const editedAgenteName = ref('')

onMounted(() => {
    fetchAgentes()
})

export async function fetchAgentes(){
    console.log('Enviando solicitud de GET a: ', apiURL)
    try{
        const url = `${apiURL}`
        const response = await fetch(url)

        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al obtener los Agentes')
        const data = await response.json()
        console.log('Agentes obtenidos: ', data)
        return data
    }catch(error){
        console.error('Error fetching agentes: ', error)
        return []
    }
}

export async function addAgente(agente){
    console.log('Función addAgente ejecutada, newAgente: ', agente)
    if(!agente.nombre.trim() || !agente.rango.trim() || !agente.activo.trim() || !agente.equipoId.trim()){
        console.log('No se añadió agente: el input está vacío')
        return false;
    }

    const activoBoolean = agente.activo.trim().toLowerCase() === 'sí' || agente.activo.trim().toLowerCase() === 'true'

    const item = {
        nombre: agente.nombre.trim(),
        rango: agente.rango.trim(),
        activo: activoBoolean,
        equipoId: agente.equipoId.trim()
    }

    try{
        console.log('Enviando solicitud POST a: ', apiURL)
        console.log('Cuerpo de la solicitud: ', JSON.stringify(item))
        const response = await fetch(apiURL, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al añadir el agente')
        newAgente.value = {nombre:'', rango:'', activo:'', equipoId:''}
        return true
    }catch(error){
        console.error('Error añadiendo agente: ', error)
        return false
    }
}

export async function removeAgente(agenteId){
    try{
        console.log('Enviando solicitud de DELETE a: ', `${apiURL}/${agenteId}`)
        const response = await fetch(`${apiURL}/${agenteId}`, {
            method: 'DELETE'
        })

        if(!response.ok) throw new Error('Error al eliminar el agente')
    }catch(error){
        console.error('Error eliminando el agente: ', error)
    }
}

export async function updateAgente(agente){
    const updatedAgente = {
        id: agente.id,
        nombre: agente.nombre,
        rango: agente.rango,
        activo: agente.activo,
        equipoId: agente.equipoId
    }

    try{
        console.log('Enviando solicitud PUT a: ', `${apiURL}/${agente.id}`)
        const response = await fetch(`${apiURL}/${agente.id}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedAgente)
        })
        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al actualizar el agente')
        editingAgenteId = null
        editedAgenteName = ''
    }catch(error){
        console.error('Error updating agente: ', error)
    }
}