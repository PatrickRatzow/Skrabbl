<template>
  <div class="rows">
    <PlayerItem v-for="(player, idx) in players"
                :key="player.id"
                :name="player.name"
                :color="player.color"
                :number="idx"
                :owner="player.owner"
    />
  </div>
</template>

<script>
import PlayerItem from "@/components/player/PlayerItem";

export default {
  name: "PlayerList",
  props: {
    players: Array
  },
  components: {
    PlayerItem
  }
}
</script>

<style scoped>

</style>