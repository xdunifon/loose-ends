<script setup>
import { useGameStore } from '@/stores/gameStore.js'

const gameStore = useGameStore()

const toggle = (key) => {
  gameStore[key] = !gameStore[key]
}
</script>

<template>
  <div class="flex flex-wrap gap-2 text-xs mb-5">
    <button v-for="key in [
      'isHost',
      'inLobby',
      'inGame',
      'inRound',
      'inVoting',
      'inLeaderboard',
      'inGameOver'
    ]" :key="key" @click="toggle(key)" :class="[
      'px-2 py-1 rounded text-white',
      gameStore[key] ? 'bg-green-600' : 'bg-red-600'
    ]">
      {{ key }}
    </button>
  </div>
</template>
