<template>
    <div id="app">
        <Navbar/>
        <div v-if="$root.loading" class="loading-circle is-flex is-justify-content-center is-align-items-flex-center">
            <LoadingCircle/>
        </div>
        <RouterView v-else/>
        <Modals/>
    </div>
</template>

<script>
import Navbar from "@/components/Navbar";
import Modals from "@/components/Modals";
import LoadingCircle from "@/components/LoadingCircle";

export default {
    components: {
        Modals,
        Navbar,
        LoadingCircle
    },
    mounted() {
        this.$store.dispatch("user/loadUser")
    }
}
</script>

<style scoped>
.loading-circle {
    width: 100vw;
    height: calc(100vh - 52px);
}
</style>

