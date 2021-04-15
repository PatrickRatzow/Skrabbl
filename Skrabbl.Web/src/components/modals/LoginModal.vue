<template>
    <div class="container">
        <div class="modal is-active">
            <div class="modal-background"></div>
            <div class="modal-content">
                <form class="box">
                    <div class="field has-text-danger" v-if="error">
                        <b>Error!</b> {{ error }}
                    </div>
                    <div class="field">
                        <label for="username" class="label">Username</label>
                        <div class="control has-icons-left">
                            <input type="text" id="username" class="input" placeholder="John Smith" v-model="username"
                                   required>
                            <span class="icon is-small is-left">
                                <i class="fa fa-user"/>
                            </span>
                        </div>
                    </div>
                    <div class="field">
                        <label for="password" class="label">Password</label>
                        <div class="control has-icons-left">
                            <input type="password" id="password" class="input" placeholder="********" v-model="password"
                                   required>
                            <span class="icon is-small is-left">
                                <i class="fa fa-lock"/>
                            </span>
                        </div>
                    </div>
                    <div class="field">
                        <label class="checkbox">
                            <input type="checkbox" v-model="rememberMe">
                            Remember Me
                        </label>
                    </div>
                    <div class="field">
                        <button class="button is-success" :class="{ 'is-loading': isLoading }" @click.prevent="login">
                            Login
                        </button>
                    </div>
                </form>
            </div>

            <button @click="setLoginModalVisible(false)" class="modal-close is-large" aria-label="close"></button>
        </div>
    </div>
</template>

<script>
import { mapActions } from "vuex"

export default {
    name: "LoginModal",
    data() {
        return {
            username: "",
            password: "",
            rememberMe: false,
            isLoading: false,
            error: ""
        }
    },
    methods: {
        ...mapActions("user", {
            setLoginModalVisible: "setLoginModalVisible"
        }),
        async login() {
            this.error = ""
            this.isLoading = true;

            const data = {
                Username: this.username,
                Password: this.password
            }

            const resp = await fetch("/api/user/login", {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Content-Type": "application/json"
                }
            })

            if (resp.status === 200) {
                await this.$store.dispatch("user/login", {
                    username: this.username,
                    jwt: await resp.text(),
                    rememberMe: this.rememberMe
                })
            } else {
                this.error = await resp.text();
            }

            this.isLoading = false
        }
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