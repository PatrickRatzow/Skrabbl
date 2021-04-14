<template>
  <button class="button"
          :class="{ 'is-success': isActive }"
          @click="$store.dispatch('canvas/setSize', size)"
  >
    {{ size }}
  </button>
</template>

<script>
export default {
  name: "SizeButton",
  props: {
    size: Number
  },
  computed: {
    isActive() {
      return this.$store.getters["canvas/size"] === this.size
    }
  }
}
</script>

<style scoped>
</style>