<script src="../store/modules/user.js"></script>
<template>
    <ul class="is-flex is-flex-direction-column">
        <li class="pl-3 pr-3 pt-1 pb-1 is-flex is-justify-content-space-between" v-for="player in sortedPlayers">
            <span class="has-text-weight-bold">{{ player.name }}</span>
            <span>{{ player.score }}</span>
        </li>
    </ul>
</template>

<script>
export default {
    name: "PointList",
    computed: {
        sortedPlayers() {
            return this.$store.state.game.players.sort((a, b) => {
                if (a.score < b.score) return 1
                if (a.score > b.score) return -1

                return 0
            });
        }
    }

}
</script>

<style scoped>
li:nth-child(2n) {
    background: rgb(242, 242, 242);
}

ul {
    margin: -0.25rem;
}
</style>