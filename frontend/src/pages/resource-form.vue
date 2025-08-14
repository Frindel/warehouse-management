<script setup>
import { reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useResourcesStore } from "@/stores/resources.js";

const resourcesStore = useResourcesStore();
const router = useRouter();

const props = defineProps({
  mode: { type: String, required: true },
  id: {
    type: String,
    validator: (value, props) => !(props.mode === 'edit' && !value)
  }
});

const isEdit = computed(() => props.mode === "edit");

const resource = reactive({
  id: props.id || '',
  name: '',
  isArchived: false
});

onMounted(() => {
  if (!isEdit.value) return;
  const selectedResource = resourcesStore.getResource(props.id);
  if (selectedResource) Object.assign(resource, selectedResource);
});

const saveResource = async () => {
  if (!isEdit.value) await resourcesStore.create(resource.name);
  else await resourcesStore.update(resource);
  await router.push({ name: 'resources' });
};

const deleteResource = async () => {
  await resourcesStore.delete(props.id);
  await router.push({ name: 'resources' });
};

const toArchive = async () => {
  resource.isArchived = true;
  await resourcesStore.update(resource);
  await router.push({ name: 'resources' });
};

const toWork = async () => {
  resource.isArchived = false;
  await resourcesStore.update(resource);
  await router.push({ name: 'resources' });
};
</script>

<template>
  <article class="content px-4">
    <h2>Ресурсы</h2>
    <div class="actions">
      <button @click="saveResource" class="btn btn-success">Сохранить</button>
      <template v-if="isEdit">
        <button @click="deleteResource" class="btn btn-danger">Удалить</button>
        <button v-if="resource.isArchived" @click="toWork" class="btn btn-primary">В работу</button>
        <button v-else @click="toArchive" class="btn btn-warning">В архив</button>
      </template>
    </div>
    <br>
    <table>
      <tbody>
      <tr>
        <td>Наименование</td>
        <td><input v-model="resource.name" class="form-control" /></td>
      </tr>
      </tbody>
    </table>
  </article>
</template>