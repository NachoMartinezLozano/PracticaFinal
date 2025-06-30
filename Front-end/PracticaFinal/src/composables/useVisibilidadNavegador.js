import { ref, onMounted, onUnmounted} from 'vue'

export function useVisibilidadNavegador() {
    const showNavegador = ref(window.innerWidth >= 768)

    const updateNavegadorVisibility = () => {
        showNavegador.value = window.innerWidth >= 768
    }

    onMounted(() => {
        window.addEventListener('resize', updateNavegadorVisibility)
    })

    onUnmounted(() => {
        window.removeEventListener('resize', updateNavegadorVisibility)
    })

    return {
        showNavegador,
        updateNavegadorVisibility,
    }
}