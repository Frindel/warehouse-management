<script setup>
import { computed, onMounted, reactive } from 'vue'
import { useUnitsStore } from '@/stores/units.js'
import { useRouter } from 'vue-router'
import { useToastStore } from '@/stores/toast.js'
import useVuelidate from '@vuelidate/core'
import { required, minLength } from '@vuelidate/validators'

const unitsStore = useUnitsStore()
const toast = useToastStore()
const router = useRouter()

const props = defineProps({
  mode: { type: String, required: true },
  id: {
    type: String,
    validator: (value, props) => !(props.mode === 'edit' && !value)
  }
})

const isEdit = computed(() => props.mode === 'edit')

const unit = reactive({
  id: props.id || '',
  name: '',
  isArchived: false
})

const rules = {
  name: { required, minLength: minLength(3) }
}
const v$ = useVuelidate(rules, unit)

onMounted(() => {
  if (!isEdit.value) return
  const selected = unitsStore.getUnit(props.id)
  if (selected) Object.assign(unit, selected)
})

const saveUnit = async () => {
  const valid = await v$.value.$validate()
  if (!valid) {
    toast.show('Исправьте ошибки в форме')
    return
  }
  try {
    if (!isEdit.value) {
      await unitsStore.create(unit.name)
    } else {
      await unitsStore.update(unit)
    }
    await router.push({ name: 'units' })
  } catch {
    // ошибки уже показаны тостами
  }
}
</script>

<template>
  <article class="content px-4">

    <h2>Ресурсы</h2>
    <div class="actions">
      <button @click="saveUnit"
              class="btn btn-success">Сохранить
      </button>
      <template v-if="isEdit">
        <button @click="deleteUnit" class="btn btn-danger">Удалить</button>
        <button v-if="unit.isArchived"
                @click="toWork"
                class="btn btn-primary">В работу
        </button>
        <button v-else
                @click="toArchive"
                class="btn btn-warning">В архив
        </button>
      </template>
    </div>
    <br>
    <div>
      <table>
        <tbody>
        <tr>
          <td>Наименование</td>
          <td>
            <input class="form-control" v-model="unit.name"/>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </article>
</template>