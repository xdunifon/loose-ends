<script setup>
import GameTimer from '@/components/Common/GameTimer.vue'
import { roundService } from '@/services/roundService'
import { ref } from 'vue'

const props = defineProps({
  voteEndTimeStamp: { type: Date, required: true }
})

const outOfTime = ref(false)

async function endVoting() {
  await roundService.endVoting()
  outOfTime.value = true
}

</script>

<template>
  <div v-if="!outOfTime">
    <h1>Game Prompt Voting</h1>
    <GameTimer :end-time-stamp="props.voteEndTimeStamp" @timer-end="endVoting()" />
  </div>
  <div v-else>
    <h1>Prompting over</h1>
  </div>
</template>
