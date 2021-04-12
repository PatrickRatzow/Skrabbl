<template>
    <li> 
        <span v-if="message.action === 'chatMessage'">
            {{message.user}} says {{message.message}}
        </span>
        <span class="has-background-success" v-else-if="message.action === 'guessedWord'">
            {{message.user}} guessed the word!
        </span>
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
</style>


//Beskeden