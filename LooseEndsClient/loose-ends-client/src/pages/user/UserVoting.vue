<script setup>
import { Card } from 'primevue'
import { roundService } from '@/services/roundService'
import { ref } from 'vue'
import GameTimer from '@/components/Common/GameTimer.vue'

const props = defineProps({
  replies: { type: Object },
  hasVoted: { type: Boolean, default: false },
  votingEndTimeStamp: { type: Date, required: true }
})

const hasVoted = ref(props.hasVoted)
const outOfTime = ref(false)

async function submitVote(replyId) {
  await roundService.submitVote(replyId)
  hasVoted.value = true
}
</script>

<template>
  <div v-if="!hasVoted && !outOfTime">

    <h1 class="text-3xl font-bold">Voting</h1>
    <GameTimer :end-time-stamp="props.votingEndTimeStamp" @timer-end="outOfTime = true" />
    <div class="mt-3 flex flex-col gap-4">
      <div v-for="{ reply, index } in replies" :key="index" @click="submitVote(reply.id)">
        <Card>
          <template #content>{{ reply.number }}</template>
        </Card>
      </div>
    </div>

    <div class="text-center mt-20">Tap on the winning prompt reply</div>
  </div>
  <div v-else>
    <h1 v-if="hasVoted">Vote submitted</h1>
    <h1 v-else>Random vote submitted</h1>
  </div>
</template>
