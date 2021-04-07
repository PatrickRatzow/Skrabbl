<template>
  <button @click="$emit('click', size)">
    {{ size }}
  </button>
</template>

<script>
export default {
  name: "SizeButton",
  props: {
    size: Number
  }
}
</script>

<style scoped>
  button {
    background-color: #00d1b2;
  }
</style>