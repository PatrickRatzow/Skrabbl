<template>
  <div class="main is-flex is-justify-content-center is-align-items-center">
    <section class="box">
      <section class="title has-text-centered">
        <div class="content">
          <p class="title">Game Lobby</p>
          <p class="subtitle">Code: {{ code }}</p>
        </div>
      </section>
      
      <div class="content-wrapper">
        <section class="players">
          <h1 class="is-size-5">Players</h1>
          <PlayerList :players="players" />
        </section>
  
        <section class="settings">
          <!-- <h1 class="is-size-5">Settings</h1> -->
        </section>   
      </div>
    </section>
  </div>
</template>

<script>
import PlayerList from "@/components/player/PlayerList";
import SettingList from "@/components/setting/SettingList";

export default {
  name: "GameLobby",
  data() {
    return {
      code: "aBc4fE",
      players: [
        { id: 1, name: "Patrick", color: "#e74c3c" },
        { id: 2, name: "Nikolaj", color: "#9b59b6" }
      ]
    }
  },
  components: {
    PlayerList,
    SettingList
  }
}
</script>

<style scoped lang="scss">
  .main {
    min-height: 100vh;
    
    > section {
      min-height: 600px;
      min-width: 600px;
      
      > .title {
        margin-top: -1.25rem;
        margin-left: -1.25rem;
        margin-right: -1.25rem;
        
        border-top-right-radius: 6px;
        border-top-left-radius: 6px;
        background: #00d1b2;
        
        > .content {
          padding: 3rem 1.25rem;
          
          > .title {
            color: white;
          }
          > .subtitle {
            color: darken(white, 5%);
          }
        }
      }
    }
  }

  .content-wrapper {
    display: flex;

    > .players {
      margin-top: -1.5rem;
      margin-left: -1.25rem;
      margin-bottom: -1.25rem;
      
      padding: 1.25rem;
      
      height: 100%;
      width: 35%;
    }

    > .settings {
      width: 65%;
    }
  }
</style>