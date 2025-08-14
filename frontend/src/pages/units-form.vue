<script setup>
import { reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useUnitsStore } from "@/stores/units.js";

const unitsStore = useUnitsStore();
const router = useRouter();

const props = defineProps({
  mode: { type: String, required: true },
  id: {
    type: String,
    validator: (value, props) => !(props.mode === 'edit' && !value)
  }
});

const isEdit = computed(() => props.mode === "edit");

const unit = reactive({
  id: props.id || '',
  name: '',
  isArchived: false
});

onMounted(() => {
  if (!isEdit.value) return;
  const selectedUnit = unitsStore.getUnit(props.id);
  if (selectedUnit) Object.assign(unit, selectedUnit);
});

const saveUnit = async () => {
  if (!isEdit.value) await unitsStore.create(unit.name);
  else await unitsStore.update(unit);
  await router.push({ name: 'units' });
};

const deleteUnit = async () => {
  await unitsStore.delete(props.id);
  await router.push({ name: 'units' });
};

const toArchive = async () => {
  unit.isArchived = true;
  await unitsStore.update(unit);
  await router.push({ name: 'units' });
};

const toWork = async () => {
  unit.isArchived = false;
  await unitsStore.update(unit);
  await router.push({ name: 'units' });
};
</script>

<template>
  <article class="content px-4">
    <h2>Еденицы измерения</h2>
    <div class="actions">
      <button @click="saveUnit" class="btn btn-success">Сохранить</button>
      <template v-if="isEdit">
        <button @click="deleteUnit" class="btn btn-danger">Удалить</button>
        <button v-if="unit.isArchived" @click="toWork" class="btn btn-primary">В работу</button>
        <button v-else @click="toArchive" class="btn btn-warning">В архив</button>
      </template>
    </div>
    <br>
    <table>
      <tbody>
      <tr>
        <td>Наименование</td>
        <td><input v-model="unit.name" class="form-control" /></td>
      </tr>
      </tbody>
    </table>
  </article>
</template>