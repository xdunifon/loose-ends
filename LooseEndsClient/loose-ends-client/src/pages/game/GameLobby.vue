<script setup>
import PlayerCard from '@/components/PlayerCard.vue'
import Button from 'primevue/button'
import { gameService } from '@/services/gameService'
import { playerService } from '@/services/playerService'
import { onMounted, ref } from 'vue'

const players = ref([])
const timerCount = ref(-1)

async function startGameCountdown() {
  timerCount.value = 5

  const interval = setInterval(() => {
    if (timerCount.value > 0) {
      timerCount.value--
    } else {
      clearInterval(interval)
      startGame()
    }
  }, 1000)
}

async function startGame() {
  await gameService.startGame(5)
}

async function updatePlayers() {
  players.value = await playerService.getPlayers()
}

onMounted(async () => {
  updatePlayers()
  console.log(players)
})
</script>

<template>
  <h1 class="text-3xl font-bold text-center">LOOSE ENDS</h1>
  <div v-if="timerCount != -1">
    <h1 class="text-3xl font-bold text-center">{{ timerCount }}</h1>
  </div>
  <div v-else class="flex flex-col justify-center">
    <h2 class="text-2xl font-bold text-center mt-10">Players</h2>

    <PlayerCard v-for="player in players" :key="player.id" :name="player.name" />

    <Button label="Start Game" class="w-full mt-5" @click="startGameCountdown()"></Button>
  </div>
</template>
