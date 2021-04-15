<template>
    <div id="app">
        <Navbar/>
        <RouterView/>
        <Modals/>
    </div>
</template>

<script>
import Navbar from "@/components/Navbar";
import Modals from "@/components/Modals";

export default {
    components: {
        Modals,
        Navbar
    },
    mounted() {
        this.$store.dispatch("user/loadUser")
    }
}
</script>
<style lang="scss">

</style>

