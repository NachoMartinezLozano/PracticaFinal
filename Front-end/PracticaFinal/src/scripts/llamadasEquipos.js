import { ref, onMounted } from 'vue'

const apiURL = 'https://localhost:7154/api/Equipo'
let id=0
export const newEquipo = ref({
    nombre: '',
    especialidad: '',
    equipoId: 0
})
const editingEquipoId = ref(null)
const editedEquipoName = ref('')

onMounted(() => {
    fetchEquipos()
})

export async function fetchEquipos(){
    console.log('Enviando solicitud de GET a: ', apiURL)
    try{
        const url = `${apiURL}`
        const response = await fetch(url)

        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al obtener los Equipos')
        const data = await response.json()
        console.log('Equipos obtenidos: ', data)
        return data
    }catch(error){
        console.error('Error fetching equipos: ', error)
        return []
    }
}

export async function addEquipo(equipo){
    console.log('Función addEquipo ejecutada, newEquipo: ', equipo)
    if(!equipo.nombre.trim() || !equipo.especialidad.trim() || !equipo.operacionId.trim()){
        console.log('No se añadió equipo: el input está vacío')
        return false;
    }

    const item = {
        nombre: equipo.nombre.trim(),
        especialidad: equipo.especialidad.trim(),
        operacionId: equipo.operacionId.trim()
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
        if(!response.ok) throw new Error('Error al añadir el equipo')
        newEquipo.value = {nombre:'', especialidad:'', operacionId: 0}
        return true
    }catch(error){
        console.error('Error añadiendo equipo: ', error)
        return false
    }
}

export async function removeEquipo(equipoId){
    try{
        console.log('Enviando solicitud DELETE a: ', `${apiURL}/${equipoId}`)
        const response = await fetch(`${apiURL}/${equipoId}`,{
            method: 'DELETE'
        })
        if(!response.ok) throw new Error('Error al eliminar el equipo')
    }catch(error){
        console.error('Error eliminando el equipo: ', error)
    }
}

export async function updateEquipo(equipo){
    const updatedEquipo = {
        id: equipo.id,
        nombre: equipo.nombre,
        especialidad: equipo.especialidad,
        operacionId: equipo.operacionId
    }

    try{
        console.log('Enviando solicitud PUT a: ', `${apiURL}/${equipo.id}`)
        const response = await fetch(`${apiURL}/${equipo.id}`,{
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedEquipo)
        })
        console.log('Respuesta recibida: ', response.status, response.statusText)
        if(!response.ok) throw new Error('Error al actualziar el equipo')
        editingEquipoId = null
        editedEquipoName = ''
    }catch(error){
        console.error('Error updating equipo: ', error)
    }
}