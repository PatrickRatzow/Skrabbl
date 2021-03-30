<template>
  <div class="row" :style="playerColor">
    [{{ number + 1 }}] - {{ name }}
    <span v-if="owner">(Owner)</span>
  </div>
</template>

<script>
export default {
  name: "PlayerItem",
  props: {
    name: String,
    color: String,
    number: Number,
    owner: Boolean
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