<script setup>

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
          <input class="form-control" type="date">
          <input class="form-control" type="date">
        </div>
      </div>
      <div class="col">
        <div class="custom-select">
          <div class="selected-items">
            <span>Выберите</span>
          </div>
          <select style="display: none;">
            <option value="heryhty">heryhty</option>
          </select>
        </div>
      </div>
      <div class="col">
        <div class="custom-select">
          <div class="selected-items">
<!--            <span>Выберите</span>-->
            <span class="selected-tag">н/ч (нямка в час)</span>
            <span class="selected-tag">xcv</span>
          </div>
          <div class="dropdown-overlay"></div>
          <div class="dropdown-options">
            <div class="option selected">н/ч (нямка в час)</div>
            <div class="option ">ЧИСТАЯ ЭНЕРГИЯ</div>
          </div>
          <select style="display: none;">
            <option value="01988195-984d-799a-ab20-33c1a2a38704">дивайн</option>
          </select>

        </div>
      </div>
      <div class="col">
        <div class="custom-select">
          <div class="selected-items">
            <span>Выберите</span>
          </div>
          <select style="display: none;">
            <option value="01987cf5-a3ef-767b-973a-f72e517839b5">324</option>
          </select>
        </div>
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