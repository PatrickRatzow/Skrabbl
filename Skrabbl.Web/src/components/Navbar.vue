<template>
    <nav aria-label="main navigation" class="navbar has-shadow" role="navigation">
        <div class="container">
            <div class="navbar-brand">
                <router-link class="navbar-item" to="/">
                    <img src="https://www.ucn.dk/Files/Templates/Designs/ucn2015/img/ucnlogo.svg" width="75">
                </router-link>
                <a aria-expanded="false" aria-label="menu" class="navbar-burger" data-target="navbar" role="button">
                    <span aria-hidden="true"></span>
                    <span aria-hidden="true"></span>
                    <span aria-hidden="true"></span>
                </a>
            </div>

            <div id="navbar" class="navbar-menu is-spaced">
                <div class="navbar-start">
                    <router-link class="navbar-item" to="/">Home</router-link>
                    <router-link class="navbar-item" to="/game-lobby">Game Lobby</router-link>
                    <router-link class="navbar-item" to="/game">Game</router-link>
                    <router-link class="navbar-item" to="/join-lobby">Join Lobby</router-link>
                </div>

                <div class="navbar-end">
                    <div class="navbar-item">
                        <div class="buttons">
                            <div v-if="isLoggedIn">
                                <button class="button is-danger is-outlined" @click="setLogoutModalVisible(true)">
                                    Logout
                                </button>
                            </div>
                            <div v-else>
                                <button class="button is-info" @click="setRegisterModalVisible(true)">Sign Up</button>
                                <button class="button is-dark is-outlined" @click="setLoginModalVisible(true)">Log in
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </nav>
</template>

<script>
import { mapActions, mapGetters } from "vuex"

export default {
    computed: {
        ...mapGetters("user", {
            isLoggedIn: "isLoggedIn"
        })
    },
    methods: {
        ...mapActions("user", {
            setLoginModalVisible: "setLoginModalVisible",
            setRegisterModalVisible: "setRegisterModalVisible",
            setLogoutModalVisible: "setLogoutModalVisible"
        }),
        setupNavbarBurger() {
            const navbarBurger = document.querySelectorAll('.navbar-burger')[0]
            if (navbarBurger === undefined)
                return;

            navbarBurger.addEventListener("click", () => {
                const target = navbarBurger.dataset.target;
                const targetNode = document.getElementById(target);
                // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
                navbarBurger.classList.toggle('is-active');
                targetNode.classList.toggle('is-active');
            })
        }
    },
    mounted() {
        this.setupNavbarBurger();
    }
}
</script>

<style scoped>
.router-link-exact-active {
    background-color: #fafafa;
    color: #3273dc;
}
</style>