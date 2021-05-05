<template>
    <div >
        <span class="content is-size-4">
            Round {{ currentRound }} of {{ maxRounds }}  | Time: <span :class="{'has-text-danger': turnTimeLeft < 16}">{{ turnTimeLeft }}</span>
        </span>
    </div>
</template>

<script>
    import { mapState } from "vuex"
    export default {
        name: "turn",
        computed: {
            ...mapState("game", {
                currentRound: "currentRound",
                turnTimeLeft: "turnTimeLeft",
                maxRounds: "maxRounds"
            })
        }
    }
</script>

<style scoped>
</style>