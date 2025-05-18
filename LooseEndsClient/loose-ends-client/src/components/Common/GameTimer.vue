<script setup>
import { getDateDiffSeconds } from '@/utils/dateUtil'
import { ref } from 'vue'

const props = defineProps({
  endTimeStamp: { type: Date },
  seconds: { type: Number, default: 30 }
})

const emit = defineEmits(['timerEnd'])

const timerCount = ref(props.endTimeStamp ? getDateDiffSeconds(props.endTimeStamp) : props.seconds)
const timerInverval = setInterval(() => {
  if (timerCount.value > 1) {
    timerCount.value--
  } else {
    clearInterval(timerInverval)
    emit('timerEnd')
  }
}, 1000)
</script>

<template>
  <h1>Time remaining: {{ timerCount }}</h1>
</template>
