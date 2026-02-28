<script setup>
import { useGameStore } from '@/stores/gameStore'

const gameStore = useGameStore()
</script>

<template>
  <div>
    <p>PlayerPage</p>
    <p>{{ gameStore.gameCode }}</p>
  </div>
</template>
