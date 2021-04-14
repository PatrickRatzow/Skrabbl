<template>
    <div class="container">
        <div class="modal" :class="{ 'is-active': !isWordListEmpty }">
            <div class="modal-background"></div>
            <div class="modal-content">
                <!-- Any other Bulma elements you want -->
                <div class="is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
                    <div class="wordText has-text-white">
                        Choose a word
                    </div>
                    <ul class="mt-2 is-flex-direction-row is-flex">
                        <li v-for="word in wordList" class="ml-2">
                            <button class="button is-primary" @click="setMessage(word)">{{word}}</button>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapState, mapGetters} from "vuex"

    export default {
        computed: {
            ...mapGetters("game", {
                isWordListEmpty: "isWordListEmpty"
            }),
            ...mapState("game", {
                wordList: "wordList"
            })
        },
        methods: {
            setMessage(msg) {
                this.$store.dispatch("game/chooseWord", msg)
            }
        },
        mounted() {

        }
    }
</script>

<style scoped>
    .words {
        margin-left: 500px;
        margin-top: 100px;
        overflow: hidden;
        height: auto;
        width: 180px;
    }
    .wordText{
        text-align: center;
    }
</style>