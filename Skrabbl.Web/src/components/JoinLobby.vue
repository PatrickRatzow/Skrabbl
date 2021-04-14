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
            }
        },
        methods: {
            joinLobby() {
              // TODO: Implement this again
              //this.connection.invoke("JoinLobby", this.userId, this.gameCode);
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