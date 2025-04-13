<script setup>
import { Card } from 'primevue'
</script>

<template>
  <h1 class="text-3xl font-bold">Game Win</h1>

  <Card class="mt-3">
    <template #content>
      <div class="flex">
        <div>Player who won</div>
        <div class="ms-auto">100 pts</div>
      </div>
    </template>
  </Card>
</template>
