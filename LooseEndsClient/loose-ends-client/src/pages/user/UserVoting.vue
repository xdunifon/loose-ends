<script setup>
import { Card } from 'primevue'
import { useRoundStore } from '@/stores/roundStore';
import UserDefault from '@/pages/user/UserDefault.vue';
import { roundService } from '@/services/roundService';

const roundStore = useRoundStore()

async function submitVote(promptId) {
  await roundService.submitVote(promptId)
}
</script>

<template>
  <div v-if="!roundStore.hasVoted">

    <h1 class="text-3xl font-bold">Voting</h1>

    <Card class="mt-3">
      <template #content>Jason thought he'd never graduate from university until he discovered this neat little
        trick..
      </template>
    </Card>

    <Card class="mt-3 hover:bg-(--color-gray-800)!" @click="submitVote(1)">
      <template #content> Prompt answer number one </template>
    </Card>

    <Card class="mt-3 hover:bg-(--color-gray-800)!" @click="submitVote(2)">
      <template #content> Prompt answer number two </template>
    </Card>

    <div class="text-center mt-20">Tap on the winning prompt reply</div>
  </div>
  <UserDefault v-else />
</template>
