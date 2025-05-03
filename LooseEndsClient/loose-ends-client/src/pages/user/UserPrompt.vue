<script setup>
import { Card, Textarea, FloatLabel, Button } from 'primevue'
import { useRoundStore } from '@/stores/roundStore';
import UserDefault from '@/pages/user/UserDefault.vue';
import { roundService } from '@/services/roundService';
import { ref } from 'vue'

const roundStore = useRoundStore()
const promptResponse = ref('')

async function submitAnswer() {
  if (promptResponse.value === '') {
    alert('Please enter your response.')
    return
  }

  await roundService.submitAnswer(promptResponse.value)

  setTimeout(async () => {
    await roundService.startVoting();
  }, 1000)
}
</script>

<template>
  <div v-if="!roundStore.hasReplied">

    <h1 class="text-3xl font-bold">Your prompt</h1>
    <Card class="mt-3">
      <template #content>{{ roundStore.promptContent }}</template>
    </Card>

    <FloatLabel variant="in" class="mt-3">
      <Textarea id="over_label" v-model="promptResponse" rows="5" cols="30" style="resize: none"
        class="w-full"></Textarea>
      <label for="in_label">Your response</label>
    </FloatLabel>
    <Button label="Submit" class="mt-3 w-full" @click="submitAnswer"></Button>
  </div>
  <UserDefault v-else />
</template>
