import {defineStore} from 'pinia'
import api from '@/services/api.js'
import {handleApiError} from '@/stores/error-handler.js'

export const useUnitsStore = defineStore('units', {
    state: () => ({
        units: []
    }),
    getters: {
        workUnits: (state) => state.units.filter(u => !u.isArchived),
        archivedUnits: (state) => state.units.filter(u => u.isArchived)
    },
    actions: {
        getUnit(id) {
            return this.units.find(unit => unit.id === id)
        },

        async load() {
            try {
                const response = await api.get('/units')
                this.units = response.data
            } catch (error) {
                handleApiError(error, 'Не удалось загрузить единицы измерения')
            }
        },

        async create(name) {
            try {
                const response = await api.post('/units', {name})
                this.units.push({id: response.data.id, name, isArchived: false})
            } catch (error) {
                handleApiError(error, 'Не удалось создать единицу измерения')
                throw error
            }
        },

        async update(unit) {
            try {
                await api.put(`/units/`, unit)
                const index = this.units.findIndex(u => u.id === unit.id)
                if (index !== -1) this.units[index] = {...unit}
            } catch (error) {
                handleApiError(error, 'Не удалось обновить единицу измерения')
                throw error
            }
        },

        async delete(id) {
            try {
                await api.delete('/units/' + id)
                this.units = this.units.filter(u => u.id !== id)
            } catch (error) {
                handleApiError(error, 'Не удалось удалить единицу измерения')
                throw error
            }
        }
    }
})