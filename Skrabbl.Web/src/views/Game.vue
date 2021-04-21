<template>
    <div class="is-flex is-justify-content-center is-align-items-center">
        <div class="columns mt-6 mb-6">
            <div v-if="true" class="column box pr-1 pl-1 mr-4">
                <h2 class="subtitle">Players</h2>
                <PointList class=" PointList"/>
                
            </div>
            <div class="column p-0">
                <div class="box has-text-centered">
                    <h1 class="title word-hint">____</h1>
                </div>
                <div class="columns">
                    <Canvas class="ml-3 mr-3 column box canvas"/>
                    <ChatboxList class="mr-3 column box chatbox-list"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Canvas from "@/components/canvas/Canvas";
import ChatboxList from "@/components/chatbox/ChatboxList";
import PointList from "@/components/PointList";

export default {
  name: "Game",
  components: {
    Canvas,
    ChatboxList,
    PointList
  }
}
</script>

<style scoped>
.word-hint {
    letter-spacing: 0.2rem;
}

.chatbox-list {
    max-width: 250px;
}

.chatbox-list, .canvas {
    margin-bottom: 2.4rem;
}
</style>