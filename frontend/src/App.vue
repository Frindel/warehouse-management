<script setup>
import {RouterView} from 'vue-router'
import TheNavbar from "@/components/the-navbar.vue";
import {onMounted, ref} from "vue";

import {useUnitsStore} from "@/stores/units";
import {useResourcesStore} from "@/stores/resources.js";
import ToastMessage from "@/components/toast-message.vue";
import {useToastStore} from "@/stores/toast.js";

const toast = useToastStore()
const isLoaded = ref(false);

onMounted(async () => {
  await useUnitsStore().load()
  await useResourcesStore().load()
  isLoaded.value = true
});

</script>


<template>
  <div class="page">
    <toast-message v-if="toast.message" :message="toast.message" />
    <the-navbar></the-navbar>
    <main>
      <router-view v-if="isLoaded"></router-view>
    </main>
  </div>
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
