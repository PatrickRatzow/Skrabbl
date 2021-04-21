<template>
    <div class="modal is-active">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
                <h1 class="title has-text-white pb-1">Connection has been lost</h1>
                <h2 class="subtitle has-text-grey-lighter">Check your internet connection, or try to refresh your
                    browser</h2>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "ConnectionModal",
    }
</script>

<style scoped>
.modal-content {
    overflow: hidden;
}
</style>