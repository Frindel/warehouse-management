import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useToastStore = defineStore('toast', () => {
    const message = ref('')

    function show(msg, timeout = 3000) {
        message.value = msg
        setTimeout(() => {
            message.value = ''
        }, timeout)
    }

    return { message, show }
})