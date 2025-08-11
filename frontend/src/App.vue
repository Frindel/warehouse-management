<script setup>
import {RouterLink, RouterView} from 'vue-router'
import TheNavbar from "@/components/the-navbar.vue";
import Resources from "@/pages/resources.vue";
import Receipts from "@/pages/receipts.vue";
import TheResourceForm from "@/components/the-resource-form.vue";
import TheReceiptForm from "@/components/the-receipt-form.vue";
</script>

<template>
  <div class="page">
    <the-navbar></the-navbar>
    <main>
<!--      <the-receipt-form></the-receipt-form>-->
      <receipts></receipts>
    </main>
  </div>
  <!--  <RouterView/>-->
</template>

<style scoped>
.page {
  position: relative;
  display: flex;
  flex-direction: column;
}

main {
  flex: 1;
}

@media (min-width: 640px) {
  .page {
    flex-direction: row;
  }
}
</style>
