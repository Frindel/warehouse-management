<script setup>

import TheReceiptsFilter from "@/components/the-receipts-filter.vue";
import {onMounted, ref, watch} from "vue";
import {useRouter} from "vue-router";
import {useReceiptsStore} from "@/stores/receipts.js";

const filter = ref({
  from: null,
  to: null,
  units: [],
  receipts: [],
  resources: []
})
const receipts = ref([])

const router = useRouter()
const receiptsStore = useReceiptsStore()

watch(
    () => router.currentRoute.value.query,
    async (newQuery) => {
      receipts.value = await receiptsStore.getReceipts(newQuery)
    },
    {immediate: true}
)

const applyFilter = () => {
  router.push({query: {...filter.value, _t: Date.now()}});

}

const changeReceipt = (id) => {
  router.push({name: 'edit-receipt', params: {id}})
}

</script>

<template>
  <article class="content px-4">
    <h2>Поступления</h2>
    <the-receipts-filter v-model="filter"></the-receipts-filter>
    <br>
    <div class="row">
      <div class="col actions">
        <button @click="applyFilter" class="btn btn-primary">Применить</button>
        <router-link :to="{ name: 'create-receipt' }" class="btn btn-success">Добавить</router-link>
      </div>
    </div>
    <br>
    <table class="table-bordered">
      <thead>
      <tr>
        <th>Номер</th>
        <th>Дата</th>
        <th>Ресурс</th>
        <th>Единица измерения</th>
        <th>Количество</th>
      </tr>
      </thead>
      <tbody>
      <template v-for="receipt in receipts"
                :key="receipt.id">
        <tr @click="changeReceipt(receipt.id)"
            v-for="(resource, idx) in receipt.resources" :key="resource.id">
          <td v-if="idx === 0" :rowspan="receipt.resources.length">{{ receipt.number }}</td>
          <td v-if="idx === 0" :rowspan="receipt.resources.length">{{ receipt.date }}</td>
          <td>{{ resource.resourceName }}</td>
          <td>{{ resource.unitName }}</td>
          <td>{{ resource.quantity }}</td>
        </tr>
      </template>
      </tbody>
    </table>
  </article>
</template>