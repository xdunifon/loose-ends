<script setup>
import GameTimer from '@/components/Common/GameTimer.vue';
import { roundService } from '@/services/roundService';
import { ref } from 'vue'

const props = defineProps({
  roundEndTimeStamp: { type: Date, required: true }
})

const outOfTime = ref(false)

async function endPrompting() {
  await roundService.endPrompting()
  outOfTime.value = true
}

</script>

<template>
  <div v-if="!outOfTime">
    <GameTimer :end-time-stamp="props.roundEndTimeStamp" @timer-end="endPrompting()" />
  </div>
  <div v-else>
    <h1>Prompting over</h1>
  </div>
</template>
