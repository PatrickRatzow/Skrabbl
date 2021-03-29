<template>
    <div id="app">
        <Home msg=":) world!" />
        <WebSocketItem sendButtonText="Send Message"/>
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
 
</style>

