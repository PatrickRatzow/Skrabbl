<template>
    <div class="container">
        <div class="modal is-active">
            <div class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Register</p>
                    <button @click="setRegisterModalVisible(false)" class="delete" aria-label="close"/>
                </header>
                <form @submit.prevent="register()">
                    <section class="modal-card-body">
                        <div class="field has-text-danger" v-if="error">
                            <b>Error!</b> {{ error }}
                        </div>
                        <div class="field">
                            <label for="username" class="label">Username</label>
                            <div class="control has-icons-left">
                                <input type="text" id="username" class="input" placeholder="John Smith"
                                       v-model="username"
                                       required>
                                <span class="icon is-small is-left">
                                    <i class="fa fa-user"/>
                                </span>
                            </div>
                        </div>
                        <div class="field">
                            <label for="email" class="label">Email</label>
                            <div class="control has-icons-left">
                                <input type="email" id="email" class="input" placeholder="john@smith.com"
                                       v-model="email"
                                       required>
                                <span class="icon is-small is-left">
                                    <i class="fa fa-envelope"/>
                                </span>
                            </div>
                        </div>
                        <div class="field">
                            <label for="password" class="label">Password</label>
                            <div class="control has-icons-left">
                                <input type="password" id="password" class="input" placeholder="********"
                                       v-model="password"
                                       required>
                                <span class="icon is-small is-left">
                                    <i class="fa fa-lock"/>
                                </span>
                            </div>
                        </div>
                        <div class="field">
                            <label for="repeat-password" class="label">Re-Enter Password</label>
                            <div class="control has-icons-left">
                                <input type="password" id="repeat-password" class="input" placeholder="********"
                                       v-model="repeatedPassword" required>
                                <span class="icon is-small is-left">
                                    <i class="fa fa-lock"/>
                                </span>
                            </div>
                        </div>
                    </section>
                    <footer class="modal-card-foot">
                        <button class="button is-success" :class="{ 'is-loading': isLoading }"
                                @click.prevent="register">Register
                        </button>
                        <button @click="setRegisterModalVisible(false)" class="button">Cancel</button>
                    </footer>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions } from "vuex"

export default {
    name: "RegisterModal",
    data() {
        return {
            username: "",
            email: "",
            password: "",
            repeatedPassword: "",
            isLoading: false,
            error: ""
        }
    },
    methods: {
        ...mapActions("user", {
            setRegisterModalVisible: "setRegisterModalVisible"
        }),
        validateInput() {
            if (this.username.length < 4) {
                this.error = "Username is too short!"

                return false
            }
            if (this.password !== this.repeatedPassword) {
                this.error = "Passwords must be the same!"

                return false
            }

            this.error = ""

            return true
        },
        async register() {
            if (!this.validateInput())
                return;
            
            this.isLoading = true;

            const data = {
                UserName: this.username,
                Email: this.email,
                Password: this.password
            }

            const resp = await fetch("/api/user", {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Content-Type": "application/json"
                }
            })

            if (resp.status === 200) {
                await Promise.all([
                    this.$store.dispatch("user/setRegisterModalVisible", false),
                    this.$store.dispatch("user/setLoginModalVisible", true)
                ])
            } else {
                this.error = await resp.text();
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