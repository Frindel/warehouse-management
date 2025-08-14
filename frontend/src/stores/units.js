import { defineStore } from 'pinia'
import api from '@/services/api.js'
import { handleApiError } from '@/stores/error-handler.js'

export const useUnitsStore = defineStore('units', {
    state: () => ({
        units: []
    }),
    getters: {
        workUnits: (state) => state.units.filter(r => !r.isArchived),
        archivedUnits: (state) => state.units.filter(r => r.isArchived)
    },
    actions: {
        getUnit(id) {
            return this.units.find(r => r.id === id)
        },

        async load() {
            try {
                const response = await api.get('/units')
                this.units = response.data
            } catch (error) {
                handleApiError(error, 'Не удалось загрузить ресурсы')
            }
        },

        async create(name) {
            try {
                const response = await api.post('/units', { name })
                this.units.push({ id: response.data.id, name, isArchived: false })
            } catch (error) {
                handleApiError(error, 'Не удалось создать ресурс')
                throw error
            }
        },

        async update(unit) {
            try {
                await api.put(`/units/`, unit)
                const index = this.units.findIndex(r => r.id === unit.id)
                if (index !== -1) this.units[index] = { ...unit }
            } catch (error) {
                handleApiError(error, 'Не удалось обновить ресурс')
                throw error
            }
        },

        async delete(id) {
            try {
                await api.delete('/units/' + id)
                this.units = this.units.filter(r => r.id !== id)
            } catch (error) {
                handleApiError(error, 'Не удалось удалить ресурс')
                throw error
            }
        }
    }
})