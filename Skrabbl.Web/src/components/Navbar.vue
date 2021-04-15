<template>
  <nav class="navbar has-shadow" role="navigation" aria-label="main navigation">
    <div class="container">
      <div class="navbar-brand">
        <router-link to="/" class="navbar-item">
          <img src="https://www.ucn.dk/Files/Templates/Designs/ucn2015/img/ucnlogo.svg" width="75">
        </router-link>
        <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbar">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbar" class="navbar-menu is-spaced">
        <div class="navbar-start">
          <router-link to="/" class="navbar-item">Home</router-link>
          <router-link to="/game-lobby" class="navbar-item">Game Lobby</router-link>
          <router-link to="/game" class="navbar-item">Game</router-link>
          <router-link to="/chat-box" class="navbar-item">ChatBox</router-link>
          <router-link to="/JoinLobby" class="navbar-item">Join Lobby</router-link>
          <router-link to="/Words" class="navbar-item">Words</router-link>
        </div>

        <div class="navbar-end">
          <div class="navbar-item">
            <div class="buttons">
              <router-link to="/register">
                <button class="button is-info">
                  Sign Up
                </button>
              </router-link>

              <router-link to="/login" class="ml-2">
                <button class="button is-dark is-outlined">
                  Log in
                </button>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  methods: {
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
</style>