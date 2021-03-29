<template>
    <section class="hero is-primary">
        <div class="hero-body">
            <h1 class="title">
                {{ msg }}
            </h1>
            <p class="subtitle">
                Welcome to your new single-page application, built with <a href="https://vuejs.org" target="_blank">Vue.js</a>
            </p>
        </div>
    </section>
</template>

<script>
    export default {
        name: 'Home',
        props: {
            msg: String
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

