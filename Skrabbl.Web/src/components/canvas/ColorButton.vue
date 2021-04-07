<template>
  <button :style="styling()" @click="$emit('click', backgroundColor)">
    {{ name }}
  </button>
</template>

<script>
export default {
  name: "ColorButton",
  props: {
    name: String,
    backgroundColor: String,
    textColor: String
  },
  methods: {
    styling() {
      return `
        background-color: ${this.backgroundColor};
        color: ${this.textColor};
      `
    }
  }
}
</script>

<style scoped>

</style>