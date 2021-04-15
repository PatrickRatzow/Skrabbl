<template>
    <div class="container">
        <div class="modal is-active">
            <div class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Login</p>
                    <button @click="setLoginModalVisible(false)" class="delete" aria-label="close"/>
                </header>
                <section class="modal-card-body">
                    <div class="field has-text-danger" v-if="errors.length">
                        <b>Error!</b>
                        <ul>
                            <li v-for="error in errors">
                                <span>{{ error }}</span>
                            </li>
                        </ul>
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
                </section>
                <footer class="modal-card-foot">
                    <button class="button is-success" :class="{ 'is-loading': isLoading }" @click.prevent="login">
                        Login
                    </button>
                    <button @click="setLoginModalVisible(false)" class="button">Cancel</button>
                </footer>
            </div>
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
            errors: []
        }
    },
    methods: {
        ...mapActions("user", {
            setLoginModalVisible: "setLoginModalVisible"
        }),
        async login() {
            this.errors = []
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
            } else if (resp.status === 400) {
                const json = await resp.json();

                if (json.errors) {
                    const errors = []
                    if (json.errors.Username) errors.push(json.errors.Username[0])
                    if (json.errors.Password) errors.push(json.errors.Password[0])

                    this.errors = errors
                } else {
                    this.errors = [
                        "Incorrect username/password combination for the username"
                    ]
                }
            } else {
                const error = await resp.text();

                this.errors = [
                    "Something bad went wrong!",
                    error
                ]
            }

            this.isLoading = false
        }
    }
}
</script>

<style scoped>
.modal-card {
    overflow-x: hidden;
    word-break: break-word;
    max-width: 380px;
}
</style>