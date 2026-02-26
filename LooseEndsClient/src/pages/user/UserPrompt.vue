<script setup>
import { Card, Textarea, FloatLabel, Button } from 'primevue'
import { roundService } from '@/services/roundService'
import { ref } from 'vue'
import GameTimer from '@/components/Common/GameTimer.vue'

const props = defineProps({
  prompt: { type: String, required: true },
  hasReplied: { type: Boolean, default: false },
  roundEndTimestamp: { type: Date, required: true }
})

const promptResponse = ref('')
const hasReplied = ref(props.hasReplied)
const outOfTime = ref(false)

async function submitAnswer() {
  if (promptResponse.value === '') {
    alert('Please enter your response.')
    return
  }

  await roundService.submitAnswer(promptResponse.value)
  hasReplied.value = true
}
</script>

<template>
  <div v-if="!hasReplied && !outOfTime">
    <GameTimer :end-time-stamp="props.roundEndTimestamp" @timer-end="outOfTime = true" />

    <h1 class="text-3xl font-bold">Your prompt</h1>
    <Card class="mt-3">
      <template #content>{{ props.prompt }}</template>
    </Card>

    <FloatLabel variant="in" class="mt-3">
      <Textarea id="over_label" v-model="promptResponse" rows="5" cols="30" style="resize: none"
        class="w-full"></Textarea>
      <label for="in_label">Your response</label>
    </FloatLabel>
    <Button label="Submit" class="mt-3 w-full" @click="submitAnswer"></Button>
  </div>
  <div v-else>
    <h1 v-if="hasReplied">Answer submitted</h1>
    <h1 v-else>A random answer was submitted</h1>
  </div>
</template>
