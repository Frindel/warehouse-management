import { defineStore } from 'pinia'
import api from '@/services/api.js'
import { handleApiError } from '@/stores/error-handler.js'

export const useResourcesStore = defineStore('resources', {
    state: () => ({
        resources: []
    }),
    getters: {
        workResources: (state) => state.resources.filter(r => !r.isArchived),
        archivedResources: (state) => state.resources.filter(r => r.isArchived)
    },
    actions: {
        getResource(id) {
            return this.resources.find(r => r.id === id)
        },

        async load() {
            try {
                const response = await api.get('/resources')
                this.resources = response.data
            } catch (error) {
                handleApiError(error, 'Не удалось загрузить ресурсы')
            }
        },

        async create(name) {
            try {
                const response = await api.post('/resources', { name })
                this.resources.push({ id: response.data.id, name, isArchived: false })
            } catch (error) {
                handleApiError(error, 'Не удалось создать ресурс')
                throw error
            }
        },

        async update(resource) {
            try {
                await api.put(`/resources/`, resource)
                const index = this.resources.findIndex(r => r.id === resource.id)
                if (index !== -1) this.resources[index] = { ...resource }
            } catch (error) {
                handleApiError(error, 'Не удалось обновить ресурс')
                throw error
            }
        },

        async delete(id) {
            try {
                await api.delete('/resources/' + id)
                this.resources = this.resources.filter(r => r.id !== id)
            } catch (error) {
                handleApiError(error, 'Не удалось удалить ресурс')
                throw error
            }
        }
    }
})