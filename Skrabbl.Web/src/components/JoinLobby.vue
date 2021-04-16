<template>
    <div>
        <div class="box">
            <div class="field has-text-danger" v-if="errors.length">
                <b>Error!</b>
                <ul>
                    <li v-for="error in errors">
                        <span>{{ error }}</span>
                    </li>
                </ul>
            </div>
            <input maxlength="4" type="text" v-model="gameCode" />
            <button @click="joinLobby">Join game</button>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                userId: 31,
                gameCode: "",
                errors: []
            }
        },
        methods: {
            async joinLobby() {
                this.errors = []

                if (this.gameCode == "") {
                    this.errors.push("Type in a gamecode")
                } else if (this.gameCode.length < 4){
                    this.errors.push("Valid gamecode is 4 characters")
                }else {
                    const data = {
                        UserId: this.userId,
                        LobbyCode: this.gameCode
                    }

                    const resp = await fetch("/api/user/join", {
                        method: "POST",
                        body: JSON.stringify(data),
                        headers: {
                            "Content-Type": "application/json"
                        }
                    })
                    if (resp.status == 404) {
                        this.errors.push("Not a valid gamecode")
                    }
                    if (resp.status == 400) {
                        this.errors.push("Bad request")
                    }
                }
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