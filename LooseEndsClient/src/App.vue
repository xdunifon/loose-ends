<script setup>
import { ref } from 'vue'
import { Button, InputText } from 'primevue'
import { gameService } from '@/services/gameService'
import { useGameStore } from '@/stores/gameStore'

const gameStore = useGameStore()

// Creating
const create = async () => {
  await gameService.createAsync()
  await gameStore.initSignalR()
}

// Joining
const gameCodeForm = ref('')
const playerName = ref('')
const join = async () => {
  await gameService.joinAsync(gameCodeForm.value, playerName.value)
  await gameStore.initSignalR()
}
</script>

<template>
  <div v-if="gameStore.gameCode">{{ gameStore.gameCode }}</div>
  <div v-if="!gameStore.gameCode" class="flex flex-col gap-4">
    <InputText v-model="gameCodeForm" label="Game Code" placeholder="Enter game code" />
    <InputText v-model="playerName" label="Player name" placeholder="Enter your name" />
    <Button label="Join Game" @click="join" />
    <Button label="Create game" @click="create" />
  </div>
  <div v-if="gameStore.gameCode">
    <p>Players</p>
    <p v-for="player in gameStore.players" :key="player.id">{{ player.name }}</p>
  </div>
</template>
