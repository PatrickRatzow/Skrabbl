<template>
    <div class="row">
        [{{ name }}]: {{ value }}
    </div>
</template>

<script>
    export default {
        name: "SettingItem",
        props: {
            name: String,
            value: String,
        },
    }
</script>

<style scoped>
</style>