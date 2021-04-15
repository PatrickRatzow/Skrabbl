<template>
    <div class="modal is-active">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="box">
                <h1>{{ word }}</h1><br />
                <div v-for="player in playerscores">
                    <h2>{{player.player}}: {{player.score}}</h2>
                </div>
                <button @click="click">click!</button>
            </div>
        </div>
        <button class="modal-close is-large" aria-label="close"></button>
    </div>
</template>

<script>
    export default {
        name: "RoundStatus",
        data() {
            return {
                word: "",
                playerscores: []

            }
        },
        methods: {
            click() {
                this.word = "click",
                this.playerscores = [{ player: "player1", score: "500"}, { player: "player2", score: "400"}]

            }
        }

    }
</script>

<style scoped>
</style>