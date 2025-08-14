import { defineStore } from 'pinia'
import qs from 'qs'
import api from '@/services/api.js'
import { handleApiError } from '@/stores/error-handler.js'

export const useReceiptsStore = defineStore('receipts', {
    actions: {
        async getFilterOptions() {
            try {
                const response = await api.get('/receipts/filter')
                return {
                    period: {
                        begin: response.data.from,
                        end: response.data.to,
                    },
                    receipts: response.data.receipts.map(r => ({
                        id: r.id,
                        name: r.number,
                    })),
                }
            } catch (error) {
                handleApiError(error, 'Не удалось получить фильтры поступлений')
            }
        },

        async getReceipts(filters) {
            try {
                const response = await api.get('/receipts', {
                    params: filters,
                    paramsSerializer: params => qs.stringify(params, { arrayFormat: 'repeat' })
                })
                return response.data
            } catch (error) {
                handleApiError(error, 'Не удалось загрузить поступления')
            }
        },

        async getReceipt(id) {
            try {
                const response = await api.get(`/receipts/` + id)
                return response.data
            } catch (error) {
                handleApiError(error, 'Не удалось получить поступление')
            }
        },

        async create(receipt) {
            try {
                await api.post('/receipts/', receipt)
            } catch (error) {
                handleApiError(error, 'Не удалось создать поступление')
                throw error
            }
        },

        async update(receipt) {
            try {
                await api.put('/receipts/', receipt)
            } catch (error) {
                handleApiError(error, 'Не удалось обновить поступление')
                throw error
            }
        },

        async delete(id) {
            try {
                await api.delete('/receipts/' + id)
            } catch (error) {
                handleApiError(error, 'Не удалось удалить поступление')
                throw error
            }
        }
    }
})
