<template>
    <div class="modal is-active" v-if="isRoundOverviewVisible">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="box is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
                <div class="field">
                    <h1 class="title is-10">Word was: {{ roundOverview.word }}</h1>
                </div>
                <div class="field mb-0 mt-0 pt-0 media" v-for="player in roundOverview.playerscores">
                        <figure class="media-left">
                            <p class="image is-32x32">
                                <i class="fas fa-heart"/>
                            </p>
                        </figure>
                        <div class="media-content">
                            <div class="content">
                                <h2 class="title is-5 mb-0">{{player.player}}:</h2>
                                <p :class="{ 'has-text-danger': player.score === 0, 'has-text-success': player.score > 0 }">+{{player.score}}</p>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters, mapState } from "vuex"
    export default {
        name: "RoundStatus",
        computed: {
            ...mapGetters("game", {
                isRoundOverviewVisible: "isRoundOverviewVisible"
            }),
            ...mapState("game", {
                roundOverview: "roundOverview"
            })

        }

    }
</script>

<style scoped>
    .modal-content {
        max-width: 350px;
    }
</style>