<script setup>
import {RouterLink } from 'vue-router'
</script>

<template>
  <div class="sidebar">
    <div class="top-row ps-3 navbar navbar-dark">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Управление складом</a>
      </div>
    </div>

    <div>
      <nav class="nav flex-column">
        <div class="nav-item px-3">
          <h4 style="color:white">Склад</h4>
          <router-link :to="{name: 'receipts'}" class="nav-link">Поступления</router-link>
<!--          <a href="#" class="nav-link">Поступления</a>-->
        </div>
        <div class="nav-item px-3">
          <h4 style="color:white">Справочники</h4>
          <router-link :to="{name: 'units'}" class="nav-link">Единицы измерения</router-link>
          <router-link :to="{name: 'resources'}" class="nav-link">Ресурсы</router-link>
<!--          <a href="#" class="nav-link">Единицы-->
<!--            измерения</a>-->
<!--          <a href="#" class="nav-link">Ресурсы</a>-->
        </div>
      </nav>
    </div>
  </div>
</template>

<style scoped>
.sidebar {
  background-image: linear-gradient(180deg, rgb(5, 39, 103) 0%, #3a0647 70%);
}

@media (min-width: 640px) {
  .top-row {
    justify-content: space-between;
  }

  .sidebar {
    width: 250px;
    height: 100vh;
    position: sticky;
    top: 0;
  }

  .top-row {
    position: sticky;
    top: 0;
    z-index: 1;
  }

  .top-row.auth a:first-child {
    flex: 1;
    text-align: right;
    width: 0;
  }
}

.top-row {
  min-height: 3.5rem;
  background-color: rgba(0, 0, 0, 0.4);
}

.navbar-brand {
  font-size: 1.1rem;
}

.nav-item {
  font-size: 0.9rem;
  padding-bottom: 0.5rem;
}

.nav-item:first-of-type {
  padding-top: 1rem;
}

.nav-item:last-of-type {
  padding-bottom: 1rem;
}

.nav-item a {
  color: #d7d7d7;
  border-radius: 4px;
  height: 3rem;
  display: flex;
  align-items: center;
  line-height: 3rem;
}

.nav-item a.active {
  background-color: rgba(255, 255, 255, 0.37);
  color: white;
}

.nav-item a:hover {
  background-color: rgba(255, 255, 255, 0.1);
  color: white;
}
</style>