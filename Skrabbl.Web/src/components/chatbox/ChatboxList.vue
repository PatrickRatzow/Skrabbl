<template>
    <div>
        <div>Connected {{ hasConnected ? 'Yes' : 'False' }}</div>
        <ul>
            <ChatboxItem v-for="message in messages"
                         :user="message.user"
                         :message="message.message" />
        </ul>
        <li>
            <input type="text" v-model="messageInput" />
            <button @click="sendMessage">
                send message
            </button>
        </li>
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
                userInput: "",
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
                this.connection.on("DeletedMessage", (user, message) => {
                    this.messages = this.messages.filter(msg => msg.user != user || msg.message != message);
                });
            },
            async startConnection() {
                await this.connection.start()
                await this.connection.invoke("GetAllMessages", 3);
            },
            async sendMessage() {
                this.connection.invoke("SendMessage", "Jakob", this.messageInput)
                    .catch(err => console.error(err.toString()))

                const data = {
                    Message: this.messageInput,
                    GameId: 3,
                    UserId: 25
                }
                const request = await fetch("/api/Message", {
                    method: "POST",
                    body: JSON.stringify(data),
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
            },
            getMessages() {


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
    ul {
        border: 1px solid black;
        height: 500px;
        width: 350px;
    }
</style>


//Chatbox selv + Sendknap