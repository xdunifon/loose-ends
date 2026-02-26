<script setup>
import { ref, onMounted } from 'vue'
import { useGameStore } from '@/stores/gameStore'
import apiClient from '@/services/api'

const gameStore = useGameStore()

onMounted(async () => {
  const response = await apiClient.post('create')
})
</script>

<template>
  <div>Hello world</div>
</template>
