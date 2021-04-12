<template>
    <div>
        Hello WORLD!
        <div class="container">
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-2">User</div>
                <div class="col-4"><input type="text" v-model="userInput" /></div>
            </div>
            <div class="row">
                <div class="col-2">Message</div>
                <div class="col-4"><input type="text" v-model="messageInput" /></div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-6">
                    <input type="button" @click.prevent="sendMessage" :disabled="!hasConnected" :value="sendButtonText" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-6">
                <ul>
                    <li v-for="msg in messages">
                        {{ msg.user }} says {{ msg.message }}
                        <span v-if="msg.user == 'Nikolaj'">(Admin)</span>
                        <button @click="deleteMessage(msg)">[x]</button>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                userInput: "",
                messageInput: "",
                hasConnected: false,
                connection: null,
                messages: []
            }
        },
        props: {
            sendButtonText: String
        },
        methods: {
            createConnection() {
                this.connection = new signalR.HubConnectionBuilder().withUrl("/ws/chat-Hub").build();
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
                    })
                    .catch(err => console.error(err.toString()))
            },
            sendMessage() {
                this.connection.invoke("SendMessage", this.userInput, this.messageInput)
                    .catch(err => console.error(err.toString()))
            },
            deleteMessage(msg) {
                this.connection.invoke("DeleteMessage", msg.user, msg.message)
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
</style>