<template>
    <form>
        <label>Username:</label><br />
        <input type="text" v-model = "username" /><br />
        <label>Password:</label><br />
        <input v-model = "password" type="password"/><br />
        <button @click.prevent="runLogin">Login</button>
        <div>
            <h1>status</h1>
            <h3>{{ stat }}</h3>
        </div>
    </form>
</template>

<script>
    export default {
        data() {
            return {
                username: "",
                password: "",
                stat: ""
            }
        },
        methods: {
            async runLogin() {
                const data = {
                    Username: this.username,
                    Password: this.password
                }

                const request = await fetch("/api/user/login", {
                    method: "POST",
                    body: JSON.stringify(data),
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                if (request.status === 200) {
                    this.stat = "ok"
                } else {
                    this.stat = "not ok"
                }
            }
        }
    }
</script>

<style scoped>
</style>