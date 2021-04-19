<template>
    <div id="app">
        <div id="wrapper">
            <Navbar/>
            <div v-if="$root.loading"
                 class="loading-circle is-flex is-justify-content-center is-align-items-flex-center">
                <LoadingCircle/>
            </div>
            <RouterView v-else/>
        </div>

        <Footer/>
        <Modals/>
    </div>
</template>

<script>
import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import Modals from "@/components/Modals";
import LoadingCircle from "@/components/LoadingCircle";

export default {
    components: {
        Modals,
        Navbar,
        LoadingCircle,
        Footer
    },
    mounted() {
        this.$store.dispatch("user/loadUser")
    }
}
</script>

<style scoped>
.loading-circle {
    width: 100vw;
    height: calc(100vh - 56px);
}

#app, html, body {
    display: flex;
    min-height: 100vh;
    flex-direction: column;
    background: #fafafa;
}

#wrapper {
    flex: 1;
}
</style>

