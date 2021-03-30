<template>
  <div class="row" :style="playerColor">
    [{{ number + 1 }}] - {{ name }}
  </div>
</template>

<script>
export default {
  name: "PlayerItem",
  props: {
    name: String,
    color: String,
    number: Number,
  },
  computed: {
    playerColor() {
      return `color: ${this.color};`
    }
  }
}
</script>

<style scoped>

</style>