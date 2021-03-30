<template>
  <div class="home">
    <Home msg="Welcome to Your Vue.js App"/>
    <WeatherList />
  </div>
</template>

<script>
// @ is an alias to /src
import Home from '@/components/Home.vue'
import WeatherList from "@/components/WeatherList";

export default {
  name: 'home',
  components: {
    Home,
    WeatherList
  }
}
</script>
