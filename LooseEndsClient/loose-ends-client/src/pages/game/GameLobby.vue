<script setup>
import PlayerCard from '@/components/PlayerCard.vue'
import Button from 'primevue/button'
import { gameService } from '@/services/gameService'
import { useGameStore } from '@/stores/gameStore'
import { ref } from 'vue'

const timerCount = ref(-1)
const gameStore = useGameStore()

async function startGameCountdown() {
  timerCount.value = 5

  const interval = setInterval(async () => {
    if (timerCount.value > 0) {
      timerCount.value--
    } else {
      clearInterval(interval)
      await gameService.startGame(gameStore.sessionId)
    }
  }, 1000)
}
</script>

<template>
  <h1 class="text-3xl font-bold text-center">LOOSE ENDS</h1>
  <div>GAME CODE: {{ gameStore.sessionId }}</div>
  <div v-if="timerCount != -1">
    <h1 class="text-3xl font-bold text-center">{{ timerCount }}</h1>
  </div>
  <div v-else class="flex flex-col justify-center">
    <h2 class="text-2xl font-bold text-center mt-10">Players</h2>

    <PlayerCard v-for="player in gameStore.players" :key="player" :name="player" />

    <Button label="Start Game" class="w-full mt-5" @click="startGameCountdown()"></Button>
  </div>
</template>
