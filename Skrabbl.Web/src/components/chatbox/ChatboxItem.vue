<template>
    <li v-if="message.action === 'chatMessage'">
        <span class="has-text-grey">{{ message.user }}</span>: {{ message.message }}
    </li>
    <li class="has-background-success" v-else-if="message.action === 'guessedWord'">
        {{ message.user }} guessed the word!
    </li>
</template>

<script>
    export default {
        props: {
            message: Object
        }
    }
</script>

<style scoped>
li {
    padding: 0.125rem 1rem;
}

li:nth-child(2n) {
    background: rgb(242, 242, 242);
}
</style>


//Beskeden