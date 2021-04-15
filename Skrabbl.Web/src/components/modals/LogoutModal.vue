<template>
    <div class="container">
        <div class="modal is-active">
            <div class="modal-background"></div>
            <div class="modal-content">
                <div class="box is-flex is-justify-content-center is-align-items-center is-flex-direction-column">
                    <div class="field">
                        <h1 class="title is-5">Are you sure you want to logout?</h1>
                    </div>
                    <div class="field mt-3">
                        <button class="button is-danger" @click="logout">Logout</button>
                        <button class="button ml-2 is-dark is-outlined" @click="setLogoutModalVisible(false)">Cancel
                        </button>
                    </div>
                </div>
            </div>

            <button @click="setLogoutModalVisible(false)" class="modal-close is-large" aria-label="close"></button>
        </div>
    </div>
</template>

<script>
import { mapActions } from "vuex"

export default {
    name: "LogoutModal",
    methods: {
        ...mapActions("user", {
            logout: "logout",
            setLogoutModalVisible: "setLogoutModalVisible"
        })
    }
}
</script>

<style scoped>
.modal-content {
    overflow-x: hidden;
    word-break: break-word;
    max-width: 380px;
}
</style>