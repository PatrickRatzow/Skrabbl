<template>
  <button class="button"
          :class="{ 'is-success': isActive }"
          @click="$store.dispatch('canvas/setSize', index)"
  >
    {{ size }}
  </button>
</template>

<script>
export default {
  name: "SizeButton",
  props: {
    index: Number,
    size: Number
  },
  computed: {
    isActive() {
      return this.$store.state.canvas.stroke.size === this.index
    }
  }
}
</script>

<style scoped>
</style>