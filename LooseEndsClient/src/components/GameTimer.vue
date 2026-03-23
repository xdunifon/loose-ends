<script setup>
import { useGameTimer } from '@/composables/useGameTimer'
import { ProgressBar } from 'primevue'
import { onMounted } from 'vue'

defineEmits(['timeUp'])

const props = defineProps({
  duration: {
    type: Number,
    required: false,
  },
  date: {
    type: [Date, String],
    required: false,
  },
})

const { secondsRemaining, startTimer, percentageTimeRemaining } = useGameTimer(
  props.duration,
  props.date,
)

onMounted(() => {
  startTimer(60)
})
</script>

<template>
  <ProgressBar :value="percentageTimeRemaining">{{ secondsRemaining }}</ProgressBar>
</template>
