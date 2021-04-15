<template>
  <div id="app">
    <Navbar/>
    <RouterView/>
  </div>
</template>

<script>
import Navbar from "@/components/Navbar";

export default {
  components: {
    Navbar
  }
}
</script>
<style lang="scss">

</style>

