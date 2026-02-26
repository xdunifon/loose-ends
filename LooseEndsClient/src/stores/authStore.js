import { ref } from "vue";

export const useAuthStore = defineStore('auth', () => {
    const token = ref()

    return {
        token
    }
})