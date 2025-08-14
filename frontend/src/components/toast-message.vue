<script setup>
const props = defineProps({
  message: { type: String, required: true }
})
</script>

<template>
  <div class="toastcontainer">
    <div class="toastmessage">{{ message }}</div>
  </div>
</template>

<style scoped>
.toastcontainer {
  position: fixed;
  top: 20px;
  right: 20px;
  max-width: 300px;
  z-index: 1000;
}

.toastmessage {
  background-color: darkred;
  color: #fff;
  padding: 15px 25px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  margin: 3px;
}
</style>