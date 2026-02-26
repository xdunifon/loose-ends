<script setup>
import { ref, onMounted } from 'vue'
import { useGameStore } from '@/stores/gameStore.js'

const gameStore = useGameStore()
</script>

<template>
  <div>Hello world</div>
</template>
