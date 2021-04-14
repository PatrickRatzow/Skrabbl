<template>
  <button class="button" :style="{ 
      color: textColor, 
      'background-color': color.backgroundColor, 
      opacity: isActive ? '100%' : '60%' 
  }"
          @click="$store.dispatch('canvas/setColor', color)"
  >
    {{ color.name }}
  </button>
</template>

<script>
export default {
  name: "ColorButton",
  props: {
    color: Object,
  },
  computed: {
    textColor() {
      return this.color.textColor || "white"
    },
    isActive() {
      return this.$store.getters["canvas/color"].backgroundColor === this.color.backgroundColor
    }
  }
}
</script>

<style scoped>

</style>