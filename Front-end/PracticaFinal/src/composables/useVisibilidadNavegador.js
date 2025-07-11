import { ref, onMounted, onUnmounted} from 'vue'

export function useVisibilidadNavegador() {
    const showNavegador = ref(false)

    const toggleNavegador = () => {
        showNavegador.value = !showNavegador.value
        console.log('showNavegador: ', showNavegador.value)
    }

    return{ showNavegador, toggleNavegador }
}