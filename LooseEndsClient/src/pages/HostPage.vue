<script setup>
import { useGameStore } from '@/stores/gameStore'
import { Button } from 'primevue'
import { gameService } from '@/services/gameService'

const gameStore = useGameStore()
</script>

<template>
  <div>
    <p>Host Page</p>
    <p>{{ gameStore.gameCode }}</p>
    <p>Players</p>
    <p v-for="player in gameStore.players" :key="player.id">{{ player.name }}</p>
    <Button label="Start Game" @click="gameService.startAsync" />
  </div>
</template>
