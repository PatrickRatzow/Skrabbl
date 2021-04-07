<template>
    <form>
        <label>Username:</label><br />
        <input type="text" v-model="username" /><br />
        <label>Email</label><br />
        <input type="email" v-model="email" /><br />
        <label>Password</label><br />
        <input type="password" v-model="password" /><br />
        <label>Re-enter Password</label><br />
        <input type="password" v-model="repeatedPassword" /><br />
        <button @click.prevent="register">Register</button>

        <div v-if="error.length > 0">
            <h1>Error!</h1>
            <h4>{{ error }}</h4>
        </div>
        <div>
            <h1>status</h1>
            <h4>{{ stat }}</h4>
        </div>
    </form>
</template>

<script>
    export default {
        data() {
            return {
                username: "",
                email: "",
                password: "",
                repeatedPassword: "",
                error: "",
                stat: ""
            }
        },
        methods: {
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
                    return

                const data = {
                    UserName: this.username,
                    Email: this.email,
                    Password: this.password
                }

                const request = await fetch("/api/UserRegistration", {
                    method: "POST",
                    body: JSON.stringify(data),
                    headers: {
                        "Content-Type": "application/json"
                    }
                })

                if (request.status === 200) {
                    // Good!
                    this.stat = "ok"
                } else {
                    // Bad!
                    this.stat = "not ok"
                }
            }
        }
    }
</script>

<style scoped>
</style>