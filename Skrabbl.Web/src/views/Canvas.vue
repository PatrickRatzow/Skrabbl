<template>
    <div>
        <Canvas />
        <Chatbox/>
    </div>
</template>

<script>
    import Canvas from "@/components/canvas/Canvas"
    import Chatbox from "@/components/chatbox/ChatboxList"
    export default {
        components: {
            Canvas,
            Chatbox
        }
    }
</script>

<style scoped>
</style>