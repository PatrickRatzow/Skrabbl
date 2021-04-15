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
        },

        mounted() {
            this.$store.dispatch("user/loadUser")
        }
}
</script>
<style lang="scss">

</style>

