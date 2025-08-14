<script setup>
import {computed, onMounted, ref} from "vue";
import {useUnitsStore} from "@/stores/units";
import {useRouter} from "vue-router";

const router = useRouter()
const unitsStore = useUnitsStore();

const isWork = ref(true)
const units = computed(() => isWork.value ? unitsStore.workUnits : unitsStore.archivedUnits)

const changeUnit = (id) => {
  router.push({name: 'edit-unit', params: {id}})
}
</script>

<template>
  <article class="content px-4">

    <h2>Единицы измерения</h2>

    <div class="actions">
      <button v-if="!isWork" @click="isWork = true" class="btn btn-primary">К рабочим</button>
      <template v-else>
        <router-link :to="{ name: 'create-unit' }" class="btn btn-success">Добавить</router-link>
        <button @click="isWork = false" class="btn btn-warning">К архиву</button>
      </template>
    </div>
    <br>
    <table class="table-bordered">
      <thead>
      <tr>
        <th>Наименование</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="unit in units" :key="unit.id"
          @click="changeUnit(unit.id)">
        <td>{{ unit.name }}</td>
      </tr>
      </tbody>
    </table>
  </article>
</template>