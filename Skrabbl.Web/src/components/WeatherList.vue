<template>
  <div class="p-2">
    <h1>Weather list</h1>
    <div>
      <button @click="refreshWeather">Refresh weather!</button>
    </div>
    <div class="rows m-2">
      <WeatherItem v-for="item in weather" 
        class="row is-full mb-1"
        :key="item.date"
        :date="item.date" 
        :temperature="item.temperature"
        :summary="item.summary"
      />
    </div>
  </div>
</template>

<script>
import WeatherItem from "@/components/WeatherItem";

export default {
  name: "WeatherList",
  data() {
    return {
      weather: []
    }
  },
  methods: {
    async refreshWeather() {
      const req = await fetch("/api/weather");
      const json = await req.json();
      
      this.weather = json.map(e => {
        return {
          date: e.date,
          temperature: e.temperatureC,
          summary: e.summary
        }
      })
    }
  },
  mounted() {
    this.refreshWeather()
  },
  components: {
    WeatherItem
  }
}
</script>

<style scoped>

</style>