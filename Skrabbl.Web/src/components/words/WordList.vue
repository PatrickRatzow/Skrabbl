<template>
    <div class="container">
        <button @click="isToggled = true;">hej</button>
        <div class="modal" :class="{ 'is-active': isToggled }">
            <div class="modal-background"></div>
            <div class="modal-content">
                <!-- Any other Bulma elements you want -->
                <div class="is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
                    <div class="wordText has-text-white">
                        Choose a word
                    </div>
                    <ul class="mt-2 is-flex-direction-row is-flex">
                        <li v-for="word in words" class="ml-2">
                            <button class="button is-primary" @click="setMessage(word)">{{word}}</button>
                        </li>
                    </ul>
                </div>
            </div>
            <button class="modal-close is-large" aria-label="close" @click="isToggled = false"></button>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                words: ["Cake", "hest", "spil"],
                isToggled: false,
                chosenWord: ""
            }
        },
        methods: {
            setMessage(msg) {
                this.chosenWord = msg;
                this.isToggled = false;
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