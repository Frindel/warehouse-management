<script setup>
import {computed, onMounted, ref} from "vue";
import {useResourcesStore} from "@/stores/resources";
import {useRouter} from "vue-router";

const router = useRouter()
const resourcesStore = useResourcesStore();

const isWork = ref(true)
const resources = computed(() => isWork.value ? resourcesStore.workResources : resourcesStore.archivedResources)

const changeResource = (id) => {
  router.push({name: 'edit-resource', params: {id}})
}
</script>

<template>
  <article class="content px-4">

    <h2>Ресурсы</h2>

    <div class="actions">
      <button v-if="!isWork" @click="isWork = true" class="btn btn-primary">К рабочим</button>
      <template v-else>
        <router-link :to="{ name: 'create-resource' }" class="btn btn-success">Добавить</router-link>
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
      <tr v-for="resource in resources" :key="resource.id"
          @click="changeResource(resource.id)">
        <td>{{ resource.name }}</td>
      </tr>
      </tbody>
    </table>
  </article>
</template>