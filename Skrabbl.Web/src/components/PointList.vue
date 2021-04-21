<template>
    <article class="media">
        <div>
            <div class="media-content" v-for="player in pointList">
                <div class="content pt-0 pb-1">
                    <span class="box">
                        <span class="columns is-vcentered is-pulled-left ml-3 pr-4"><strong>{{ player.player }}</strong></span>
                        <span class="columns is-vcenteres is-pulled-right mr-3 pl-6 has-text-success">{{ player.score }}</span>
                    </span>

                </div>
            </div>
        </div>
    </article>
</template>

<script>
    import { mapGetters, mapState } from "vuex"
    export default {
        name: "PointList",
        computed: {
            ...mapGetters("game", {
                isNewScoresAdded: "isNewScoresAdded"
            }),
            ...mapState("game", {
                pointList: "pointList"
            })

        }

    }
</script>

<style scoped>
</style>