<script setup>
import PlayerCard from '@/components/PlayerCard.vue'
import Button from 'primevue/button'
import { gameService } from '@/services/gameService'
import { useGameStore } from '@/stores/gameStore'
import { ref } from 'vue'
import GameTimer from '@/components/Common/GameTimer.vue'

const gameStore = useGameStore()
const gameStarting = ref(false)

async function startGame() {
  await gameService.startGame(gameStore.sessionId)
}
</script>

<template>
  <h1 class="text-3xl font-bold text-center">LOOSE ENDS</h1>
  <div>GAME CODE: {{ gameStore.sessionId }}</div>
  <div class="flex flex-col justify-center">
    <h2 class="text-2xl font-bold text-center mt-10">Players</h2>

    <PlayerCard v-for="player in gameStore.players" :key="player" :name="player" />

    <Button label="Start Game" class="w-full mt-5" @click="gameStarting = true"></Button>
  </div>
  <GameTimer v-if="gameStarting" :seconds="5" @timer-end="startGame()" />
</template>
