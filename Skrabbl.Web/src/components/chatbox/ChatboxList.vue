<template>
    <div>
        <div>Connected {{ hasConnected ? 'Yes' : 'No' }}</div>
        <div class="chatbox">
          <ul>
            <ChatboxItem v-for="message in messages"
                         :message="message" />
          </ul>
        </div>
        <form class="field mt-1">
          <label class="label mt-1">User ID</label>
          <div class="control">
            <input class="input" type="number" v-model="userId" placeholder="Text input" />
          </div>
          <label class="label">Message</label>
          <div class="control">
            <input class="input" type="text" v-model="messageInput" placeholder="Text input" />
          </div>
          <button :disabled="hasGuessed" @click.prevent="sendMessage" class="button is-primary mt-2 is-pulled-right">
            Submit
          </button>
        </form>
    </div>
</template>

<script>
    import ChatboxItem from "@/components/chatbox/ChatboxItem.vue"
    
    export default {
        components: { 
          ChatboxItem
        },
        data() {
            return {
                userId: 25,
                messageInput: "",
                hasConnected: false,
                connection: null,
                hasGuessed: false,
                messages: []
            }
        },
        methods: {
            addMessage(action, object) {
              this.messages.push({
                  action,
                  ...object
              });
            },
            createConnection() {
                this.connection = new signalR.HubConnectionBuilder().withUrl("/ws/chat-hub").build();
            },
            setupHandlers() {
                this.connection.on("ReceiveMessage", (user, message) => {
                    this.addMessage("chatMessage", {
                        user, 
                        message
                    })
                })
                this.connection.on("GuessedWord", (user) => {
                    this.addMessage("guessedWord", {
                        user
                    })
                    this.hasGuessed = true
                })
            },
            async startConnection() {
                await this.connection.start()
                this.hasConnected = true
                await this.connection.invoke("GetAllMessages", 3);
            },
            async sendMessage() {
                await this.connection.invoke("SendMessage", 3, parseInt(this.userId), this.messageInput)

            },
        },
        mounted() {
            this.createConnection()
            this.setupHandlers()
            this.startConnection()
        }
    }
</script>

<style scoped>
    .chatbox {
        overflow: hidden;
        overflow-y:auto;
        border: 1px solid black;
        height: 427px;
        width: 350px;
    }
</style>
    

//Chatbox selv + Sendknap