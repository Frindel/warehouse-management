<script setup>
import CustomSelect from "@/components/custom-select.vue";

import {useUnitsStore} from "@/stores/units.js";
import {storeToRefs} from "pinia";
import {useResourcesStore} from "@/stores/resources.js";
import {useReceiptsStore} from "@/stores/receipts.js";
import {computed, onMounted, reactive, ref, watch} from "vue";
import {useRoute, useRouter} from "vue-router";

const props = defineProps({
  modelValue: {
    type: Object,
    default: () => ({
      from: null,
      to: null,
      units: [],
      receipts: [],
      resources: []
    })
  }
})
const emit = defineEmits(['update:modelValue'])

const selectedOptions = computed({
  get: () => props.modelValue,
  set: (val) => emit("update:modelValue", val)
})

const route = useRoute()
const router = useRouter()
const receiptsStore = useReceiptsStore()

const {units} = storeToRefs(useUnitsStore())
const {resources} = storeToRefs(useResourcesStore())
const receipts = ref([])

watch(
    () => router.currentRoute.value.query,
    async (newQuery) => {
      const options = await receiptsStore.getFilterOptions()
      receipts.value = options.receipts

      selectedOptions.value = {
        from: newQuery.from || options.period.begin,
        to: newQuery.to || options.period.end,
        units: newQuery.units ? (Array.isArray(newQuery.units) ? newQuery.units : [newQuery.units]) : [],
        receipts: newQuery.receipts ? (Array.isArray(newQuery.receipts) ? newQuery.receipts : [newQuery.receipts]) : [],
        resources: newQuery.resources ? (Array.isArray(newQuery.resources) ? newQuery.resources : [newQuery.resources]) : []
      }
    },
    {immediate: true}
)

</script>

<template>
  <div>
    <div class="row">
      <div class="col">Период</div>
      <div class="col">Номер поступления</div>
      <div class="col">Ресурс</div>
      <div class="col">Единица измерения</div>
    </div>
    <div class="row">
      <div class="col">
        <div class="input-group">
          <input v-model="selectedOptions.from" class="form-control" type="date">
          <input v-model="selectedOptions.to" class="form-control" type="date">
        </div>
      </div>
      <div class="col">
        <custom-select :options="receipts" v-model="selectedOptions.receipts"></custom-select>
      </div>
      <div class="col">
        <custom-select :options="resources" v-model="selectedOptions.resources"></custom-select>
      </div>
      <div class="col">
        <custom-select :options="units" v-model="selectedOptions.units"></custom-select>
      </div>
    </div>
  </div>
</template>