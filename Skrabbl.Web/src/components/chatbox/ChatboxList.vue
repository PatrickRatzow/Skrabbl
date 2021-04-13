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
      <button :disabled="hasGuessed" @click.prevent="sendMessage" class="button is-primary mt-2 is-pulled-right">
        Submit
      </button>
    </form>
  </div>
</template>

<script>
import {mapState} from "vuex"
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
    messages: state => state.chatBox.messages,
    hasGuessed: state => state.chatBox.hasGuessed,
    isConnected: state => state.signalR.connected
  }),
  methods: {
    sendMessage() {
      this.$store.dispatch("chatBox/sendMessage", this.messageInput)
    },
  },
  mounted() {
    this.$store.dispatch("chatBox/fetchMessages")
  }
}
</script>

<style scoped>
.chatbox {
  overflow: hidden;
  border: 1px solid black;
  height: 427px;
  width: 350px;
}
</style>


