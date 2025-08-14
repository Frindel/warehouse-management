<script setup>
import {computed, ref, watch} from "vue";

var props = defineProps({
  options: {
    type: Array,
    required: true
  },
  modelValue: {
    type: Array,
    default: () => []
  }
})
const emit = defineEmits(["update:modelValue"]);

const selected = computed({
  get: () => props.modelValue,
  set: (val) => emit("update:modelValue", val)
})
const isOpen = ref(false);

const selectedOptions = computed(() =>
    props.options.filter(opt => selected.value.includes(opt.id))
);

function toggleDropdown() {
  isOpen.value = !isOpen.value;
}

function closeDropdown() {
  isOpen.value = false;
}

const toggleOption = (id) => {
  const index = selected.value.indexOf(id);
  if (index === -1) {
    selected.value.push(id);
  } else {
    selected.value.splice(index, 1);
  }
}

</script>

<template>
  <div @click.stop="toggleDropdown" class="custom-select">
    <div class="selected-items">
      <span v-if="!selected.length">Выберите</span>
      <span v-for="option in selectedOptions"
            :key="option.id"
            class="selected-tag">
        {{ option.name }}
      </span>
    </div>
    <div v-if="isOpen" @click.stop="closeDropdown" class="dropdown-overlay"></div>
    <div v-if="isOpen" class="dropdown-options">
      <div v-for="option in options"
           :key="option.id"
           :class="{selected: selected.includes(option.id)}"
           @click.stop="toggleOption(option.id)"
           class="option">
        {{ option.name }}
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Добавляем стили для overlay */
.dropdown-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 999;
  /* Ниже чем у dropdown-options */
  cursor: default;
}

.dropdown-options {
  /* Увеличиваем z-index для dropdown */
  z-index: 1000;
  /* Остальные стили без изменений */
  position: absolute;
  top: calc(100% + 4px);
  left: 0;
  right: 0;
  background: white;
  border: 1px solid #ccc;
  border-radius: 4px;
  max-height: 200px;
  overflow-y: auto;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.custom-select {
  position: relative;
  font-family: Arial;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
  /* Уменьшен размер шрифта */
}

.selected-items {
  min-height: 36px;
  /* Уменьшена минимальная высота */
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  cursor: pointer;
  padding: 4px 28px 4px 8px;
  /* Уменьшены отступы */
  position: relative;
}

.selected-tag {
  background: #e0e0e0;
  padding: 2px 6px;
  /* Уменьшен padding */
  border-radius: 3px;
  font-size: 0.85em;
  /* Уменьшен размер шрифта */
  line-height: 1.4;
  /* Выравнивание по вертикали */
  margin: 1px 0;
}

.placeholder {
  color: #999;
  align-self: center;
  padding: 4px 0;
}

.dropdown-options {
  position: absolute;
  top: calc(100% + 4px);
  /* Поднят ближе к инпуту */
  left: 0;
  right: 0;
  background: white;
  border: 1px solid #ccc;
  border-radius: 4px;
  max-height: 200px;
  overflow-y: auto;
  z-index: 1000;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.option {
  padding: 6px 12px;
  /* Уменьшен padding */
  cursor: pointer;
  line-height: 1.4;
}

.option:hover {
  background-color: #f5f5f5;
}

.option.selected {
  background-color: #e8f0fe;
  font-weight: bold;
}

/* Добавим стрелку как у нативного select */
.selected-items:after {
  content: "▼";
  position: absolute;
  right: 8px;
  top: 50%;
  transform: translateY(-50%);
  font-size: 0.7em;
  color: #666;
  pointer-events: none;
}

.custom-select.open .selected-items:after {
  transform: translateY(-50%) rotate(180deg);
}
</style>