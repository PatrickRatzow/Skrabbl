<template>
    <div>
        <input maxlength="4" type="text" v-model="gameCode"/>
        <button @click="joinLobby">Join game</button>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                userId: 25,
                gameCode: "",
                connection: null
            }
        },
        mounted() {
            this.createConnection()
            this.startConnection()
        },
        methods: {
            createConnection() {
                this.connection = new signalR.HubConnectionBuilder().withUrl("/ws/connectToLobby-hub").build();
            },
            async startConnection() {
                await this.connection.start()

                this.connected = true
            },
            joinLobby() {
                this.connection.invoke("JoinLobby", this.userId, this.gameCode);
            }
        }
    }
</script>

<style scoped>
    button {
        color: black;
        height: 30px;
        width: 100px;
        margin-right: 10px;
        font-size: 16px;
    }

    input {
        width: 75px;
        height: 30px;
        margin-right: 10px;
        font-size: 16px;
    }
</style>