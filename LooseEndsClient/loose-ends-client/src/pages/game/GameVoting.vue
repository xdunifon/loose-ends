<script setup>
import { roundService } from '@/services/roundService'
import { ref, onMounted } from 'vue'
import { useRoundStore } from '@/stores/roundStore'
const roundStore = useRoundStore()

const votingTimer = ref(-1)

function startVotingTimer() {
  votingTimer.value = roundStore.voteTime

  const interval = setInterval(() => {
    if (votingTimer.value > 0) {
      votingTimer.value--
    } else {
      clearInterval(interval)
      roundService.endRound()
    }
  }, 1000)
}

onMounted(async () => {
  startVotingTimer()
})
</script>

<template>
  <h1>Game Prompt Voting</h1>
  <div class="text-3xl text-center mt-5">{{ votingTimer }}</div>
</template>
