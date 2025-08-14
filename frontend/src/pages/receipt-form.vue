<script setup>
import { reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useReceiptsStore } from "@/stores/receipts.js";
import { useResourcesStore } from "@/stores/resources.js";
import { useUnitsStore } from "@/stores/units.js";

const receiptsStore = useReceiptsStore();
const router = useRouter();
const resourcesStore = useResourcesStore();
const unitsStore = useUnitsStore();

const props = defineProps({
  mode: { type: String, required: true },
  id: {
    type: String,
    validator: (value, props) => !(props.mode === 'edit' && !value)
  }
});

const isEdit = computed(() => props.mode === "edit");

const receipt = reactive({
  id: props.id || '',
  number: '',
  date: new Date().toISOString().split('T')[0],
  resources: []
});

const resources = resourcesStore.workResources;
const units = unitsStore.workUnits;

onMounted(async () => {
  if (!isEdit.value) return;
  const selectedReceipt = await receiptsStore.getReceipt(props.id);
  if (selectedReceipt) Object.assign(receipt, selectedReceipt);
});

const addResource = () => {
  const item = {
    resource: resources[0]?.id || '',
    unit: units[0]?.id || '',
    quantity: 0
  };
  receipt.resources.push(item);
};

const removeResource = idx => receipt.resources.splice(idx, 1);

const saveReceipt = async () => {
  if (!isEdit.value) await receiptsStore.create(receipt);
  else await receiptsStore.update(receipt);
  await router.push({ name: 'receipts' });
};

const deleteReceipt = async () => {
  await receiptsStore.delete(props.id);
  await router.push({ name: 'receipts' });
};
</script>

<template>
  <article class="content px-4">
    <h2>Поступление</h2>
    <div class="actions">
      <button @click="saveReceipt" class="btn btn-success">Сохранить</button>
      <button @click="deleteReceipt" class="btn btn-danger">Удалить</button>
    </div>
    <br>
    <table>
      <tbody>
      <tr>
        <td>Номер</td>
        <td><input v-model="receipt.number" class="form-control" /></td>
      </tr>
      <tr>
        <td>Дата</td>
        <td><input type="date" v-model="receipt.date" class="form-control" /></td>
      </tr>
      </tbody>
    </table>
    <br>
    <table class="table-bordered">
      <thead>
      <tr>
        <td><button @click="addResource" class="btn btn-success">+</button></td>
        <th>Ресурс</th>
        <th>Единица измерения</th>
        <th>Количество</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="(res, idx) in receipt.resources" :key="idx">
        <td><button @click="removeResource(idx)" class="btn btn-danger">x</button></td>
        <td>
          <select v-model="res.resource" class="form-control">
            <option v-for="r in resources" :key="r.id" :value="r.id">{{ r.name }}</option>
          </select>
        </td>
        <td>
          <select v-model="res.unit" class="form-control">
            <option v-for="u in units" :key="u.id" :value="u.id">{{ u.name }}</option>
          </select>
        </td>
        <td><input v-model="res.quantity" class="form-control" type="number" /></td>
      </tr>
      </tbody>
    </table>
  </article>
</template>