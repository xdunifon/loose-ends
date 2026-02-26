<script setup>
import { Card } from 'primevue'
import { playerService } from '@/services/playerService'
import { roundService } from '@/services/roundService'
import { ref, computed, onMounted } from 'vue'

const players = ref([])

onMounted(async () => {
  players.value = await playerService.getPlayers()

  setTimeout(() => {
    roundService.startRound()
  }, 1000)
})

// Sorted players by points (highest first)
const sortedPlayers = computed(() =>
  [...players.value].sort((a, b) => b.points - a.points)
)
</script>

<template>
  <h1 class="text-3xl font-bold">Leaderboard</h1>

  <Card v-for="(player, index) in sortedPlayers" :key="player.id" class="mt-3">
    <template #content>
      <div class="flex">
        <div>{{ index + 1 }}</div>
        <div class="ms-2">{{ player.name }}</div>
        <div class="ms-auto">{{ player.points }}</div>
      </div>
    </template>
  </Card>
</template>
