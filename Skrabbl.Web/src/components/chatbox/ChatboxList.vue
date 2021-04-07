<template>
    <div>
        <ul>
            <ChatboxItem v-for="message in messages"
                         :user = "message.user"
                         :message = "message.message"
                         />
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
        components: { ChatboxItem },
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
            createConnection() {
                this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
            },
            setupHandlers() {
                this.connection.on("ReceiveMessage", (user, message) => {
                    this.messages.push({
                        user,
                        message
                    })
                });
                this.connection.on("DeletedMessage", (user, message) => {
                    this.messages = this.messages.filter(msg => msg.user != user || msg.message != message);
                });
            },
            startConnection() {
                
                this.connection.start()
                    .then(() => {
                        this.hasConnected = true
                        this.connection.invoke("GetAllMessages", "123");
                    })
                    .catch(err => console.error(err.toString()))
            },
            sendMessage() {
                this.connection.invoke("SaveMessage")
                this.connection.invoke("SendMessage", "Jakob", this.messageInput)
                    .catch(err => console.error(err.toString()))
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