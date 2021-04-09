<template>
  <div class="is-flex p-3">
    <Canvas />
    <ChatboxList />
  </div>
</template>

<script>
import Canvas from "@/components/canvas/Canvas";
import ChatboxList from "@/components/chatbox/ChatboxList";

export default {
  name: "Game",
  components: {
    Canvas,
    ChatboxList
  }
}
</script>

<style scoped>

</style>