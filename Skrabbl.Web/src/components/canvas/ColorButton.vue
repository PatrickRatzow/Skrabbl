<template>
  <button class="button" :style="{ 
      color: textColor, 
      'background-color': backgroundColor, 
      opacity: isActive ? '100%' : '60%' 
  }"
          @click="$store.dispatch('canvas/setColor', index)"
  >
    {{ name }}
  </button>
</template>

<script>
export default {
  name: "ColorButton",
  props: {
    index: Number,
    name: String,
    backgroundColor: String,
    textColor: String
  },
  computed: {
    isActive() {
      return this.$store.state.canvas.stroke.color === this.index
    }
  }
}
</script>

<style scoped>

</style>