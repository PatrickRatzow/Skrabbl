<template>
    <li>{{user}} says {{message}}</li>
</template>

<script>
    export default {
        props: {
            user: String,
            message: String
        }
    }
</script>

<style scoped>
</style>


//Beskeden