<template>
    <div id="app">
        <Home msg=":) world!" />
        <WebSocketItem sendButtonText="Send Message"/>
    </div>
</template>

<script>
    import Home from './components/Home.vue';
    import WebSocketItem from './components/WebSocketItem.vue';

    export default {
        name: 'app',
        components: {
            Home,
            WebSocketItem
        }
    };
</script>

<style lang="scss">
 
</style>

