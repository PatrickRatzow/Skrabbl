<template>
  <div>
      <div v-if="!isConnected">Is not connected to SignalR!</div>
      <div class="is-flex is-flex-direction-column chat-wrapper">
          <div class="chatbox mb-3">
              <ul class="mt-1">
                  <ChatboxItem v-for="message in messages"
                               :message="message"/>
              </ul>
          </div>
          <form class="mt-2 is-flex is-flex-direction-row">
              <input :disabled="disabled"
                     class="input"
                     type="text"
                     v-model="messageInput"
                     placeholder="Message"
              />
              <button :disabled="disabled"
                      @click.prevent="sendMessage"
                      :class="{ 'is-loading': loading }"
                      class="button is-primary ml-2"
              >
                  Send
              </button>
          </form>
      </div>
  </div>
</template>

<script>
import { mapState } from "vuex"
import ChatboxItem from "@/components/chatbox/ChatboxItem.vue"

export default {
  components: {
    ChatboxItem
  },
  data() {
    return {
        messageInput: "",
        disabled: false,
        loading: false
    }
  },
    computed: mapState({
        messages: state => state.chat.messages,
        hasGuessed: state => state.chat.hasGuessed,
        isConnected: state => state.signalR.connected
    }),
    watch: {
        messages() {
            this.$nextTick(() => {
                const el = document.getElementsByClassName("chatbox")
                if (!el) return;
                if (!el[0]) return;

                el[0].scrollTop = el[0].scrollHeight;
            })
        }
    },
    methods: {
        async sendMessage() {
            if (this.loading) return

            this.loading = true
            await this.$store.dispatch("chat/sendMessage", this.messageInput)
            this.loading = false
            this.messageInput = ""
        }
    },
    mounted() {
        this.$store.dispatch("chat/fetchMessages")
    }
}
</script>

<style scoped>
.chat-wrapper {
    height: 100%;
    max-height: 598px;
}

.chatbox {
    margin: -0.75rem;
    overflow: hidden;
    overflow-y: auto;
    height: inherit;
    min-width: 230px;
}
</style>


