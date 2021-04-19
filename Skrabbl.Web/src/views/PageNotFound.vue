<template>
    <div class="full">
        <div class="is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
            <h1 class="title">404</h1>
            <h2 class="subtitle">Page Not Found</h2>
        </div>
    </div>
</template>

<script>
export default {
    name: "PageNotFound"
}
</script>

<style scoped>
.full {
    width: 100%;
    height: calc(100vh - 56px);
}

.full > div {
    height: 100%;
}
</style>