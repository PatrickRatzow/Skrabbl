<template>
  <div>
    <div>Has connected {{ isConnected ? 'Yes' : 'No' }}</div>
    <div class="chatbox">
      <ul>
        <ChatboxItem v-for="message in messages"
                     :message="message"/>
      </ul>
    </div>
    <form class="field mt-1">
      <label class="label mt-1">User ID</label>
      <div class="control">
        <input class="input" type="number" v-model="userId" placeholder="Text input"/>
      </div>
      <label class="label">Message</label>
      <div class="control">
        <input class="input" type="text" v-model="messageInput" placeholder="Text input"/>
      </div>
      <button :disabled="hasGuessed" @click.prevent="sendMessage(messageInput)"
              class="button is-primary mt-2 is-pulled-right">
        Submit
      </button>
    </form>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex"
import ChatboxItem from "@/components/chatbox/ChatboxItem.vue"

export default {
  components: {
    ChatboxItem
  },
  data() {
    return {
      userId: 25,
      messageInput: "",
    }
  },
  computed: mapState({
    messages: state => state.chat.messages,
    hasGuessed: state => state.chat.hasGuessed,
    isConnected: state => state.signalR.connected
  }),
  methods: mapActions({
    sendMessage: "chat/sendMessage"
  }),
  mounted() {
    this.$store.dispatch("chat/fetchMessages")
  }
}
</script>

<style scoped>
.chatbox {
  overflow: hidden;
  overflow-y: auto;
  border: 1px solid black;
  height: 427px;
  width: 350px;
}
</style>


