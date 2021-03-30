<template>
  <div>
    <h3>{{ formattedDate }} - {{ summary }}</h3>
    <p>The temperature will be {{ temperature }}</p>
  </div>
</template>

<script>
export default {
  name: "WeatherItem",
  props: {
    date: String,
    temperature: Number,
    summary: String
  },
  computed: {
    formattedDate() {
      return new Date(this.date).toLocaleDateString()
    }
  }
}
</script>

<style scoped>

</style>