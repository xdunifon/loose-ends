<script setup>
import Card from 'primevue/card'

const props = defineProps({
  name: { type: String, required: true },
})
</script>

<template>
  <Card class="mt-3">
    <template #title>{{ props.name }}</template>
  </Card>
</template>
