<template>
    <div id="app">
        <Home msg="Hello world!" />
    </div>
</template>

<script>
    import Home from './components/Home.vue';

    export default {
        name: 'app',
        components: {
            Home
        }
    };
</script>

<style lang="scss">
    $primary: rgba(0, 0, 255, 0.739);

    body {
        color: $primary;
    }
</style>

