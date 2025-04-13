<script setup>
import { useRoundStore } from '@/stores/roundStore.js'
import { roundService } from '@/services/roundService'
import { ref, onMounted } from 'vue'

const roundStore = useRoundStore()
const timerCount = ref(-1)
console.log(`Starting round ${roundStore.roundNumber}`);

async function startPromptTimer() {
  timerCount.value = roundStore.promptTime

  const interval = setInterval(async () => {
    if (timerCount.value > 0) {
      timerCount.value--
    } else {
      clearInterval(interval)
      await roundService.startVoting()
    }
  }, 1000)
}

onMounted(async () => {
  await startPromptTimer()
})
</script>

<template>
  <div class="text-3xl text-center">Game Timer</div>
  <div class="text-3xl text-center mt-5">{{ timerCount }}</div>
</template>
