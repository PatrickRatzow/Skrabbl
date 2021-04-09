<template>
    <div>
        <div>Connected {{ hasConnected ? 'Yes' : 'No' }}</div>
        <div class="chatbox">
          <ul>
            <ChatboxItem v-for="message in messages"
                         :user="message.user"
                         :message="message.message" />
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
          <button @click.prevent="sendMessage" class="button is-primary mt-2 is-pulled-right">
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
                messages: []
            }
        },
        methods: {
            addMessage(user, message) {
              this.messages.push({
                user,
                message
              });
            },
            createConnection() {
                this.connection = new signalR.HubConnectionBuilder().withUrl("/ws/chat-hub").build();
            },
            setupHandlers() {
                this.connection.on("ReceiveMessage", this.addMessage)
            },
            async startConnection() {
                await this.connection.start()
                this.hasConnected = true
                await this.connection.invoke("GetAllMessages", 3);
            },
            async sendMessage() {
                await this.connection.invoke("SendMessage", 3, parseInt(this.userId), this.messageInput)
            }
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
        border: 1px solid black;
        height: 427px;
        width: 350px;
    }
</style>


//Chatbox selv + Sendknap