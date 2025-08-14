import { useToastStore } from '@/stores/toast.js'

export function handleApiError(error, defaultMsg) {
    const toast = useToastStore()
    let message = defaultMsg

    if (error.response) {
        switch (error.response.status) {
            case 400:
                message = 'Ошибка в данных запроса'
                break
            case 404:
                message = 'Ресурс не найден'
                break
            case 409:
                message = 'Такой элемент уже существует'
                break
            case 423:
                message = 'Элемент используется'
                break
        }
        if (error.response.data?.errors) {
            // Если сервер вернул ошибки валидации по полям
            message += ' — ' + Object.values(error.response.data.errors).flat().join(', ')
        }
    }

    toast.show(message)
}