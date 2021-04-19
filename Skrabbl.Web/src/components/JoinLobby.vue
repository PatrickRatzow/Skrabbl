<template>
    <div class="main is-flex is-justify-content-center is-align-items-flex-start">
        <div class="box mt-6">
            <div class="field has-text-danger" v-if="errors.length">
                <b>Error!</b>
                <ul>
                    <li v-for="error in errors">
                        <span>{{ error }}</span>
                    </li>
                </ul>
            </div>
            <div>
                <label for="lobby-code" class="label">Lobby Code</label>
                <div class="control has-icons-left">
                    <input
                            type="text"
                            id="lobby-code"
                            class="input"
                            maxlength="4"
                            minlength="4"
                            placeholder="0xXg"
                            v-model="lobbyCode"
                            required
                    >
                    <span class="icon is-small is-left">
                        <i class="fa fa-code"/>
                    </span>
                </div>
                <button class="mt-2 button is-primary" @click="joinLobby">Join Game Lobby</button>
            </div>
        </div>
    </div>
</template>

<script>
import LobbyService from "@/services/lobby.service"

export default {
    data() {
        return {
            lobbyCode: "",
            errors: []
        }
    },
    methods: {
        async joinLobby() {
            this.errors = []

            if (this.lobbyCode.length !== 4) {
                this.errors.push("Your lobby code must be 4 characters long!")

                return
            }

            try {
                const resp = await LobbyService.joinLobby(this.lobbyCode);
                this.errors.push("Not an error! Found a lobby!")
                this.errors.push(resp.data)
            } catch (err) {
                if (err.response.status === 404) {
                    this.errors.push("Unable to find that lobby")
                } else if (err.response.status === 403) {
                    this.errors.push("You're not allowed to do that! You're already in a lobby")
                } else if (err.response.status === 400) {
                    this.errors.push("Malformed request!")
                } else {
                    this.errors.push(`Unexpected error. Code: ${err.response.status}`)
                }
            }
        }
    }
}
</script>

<style scoped>
.box {
    min-width: clamp(0px, calc(100vw - 2rem), 500px);
    max-width: 600px;
}

.main {
    min-height: calc(100vh - 56px);
}
</style>