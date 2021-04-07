<template>
  <div id="app">
      <div id="nav">
          <router-link to="/">Home</router-link> |
          <router-link to="/about">About</router-link> |
          <router-link to="/websocket">WebSocket</router-link> |
          <router-Link to="/Canvas">Canvas</router-Link> |
          <router-link to="/game-lobby">Game Lobby</router-link> |
          <router-link to="/game">Game</router-link>    |
          <router-link to="/Login">Login</router-link>  |
          <router-link to="/Register">Register</router-link>  |
          <router-link to="/chat-box">ChatBox</router-link>
      </div>
    <router-view/>
  </div>
</template>

<style lang="scss">
 
</style>

