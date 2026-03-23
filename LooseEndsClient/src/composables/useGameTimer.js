import { computed, ref } from 'vue'

export const useGameTimer = (duration, date) => {
  const secondsRemaining = ref(0)
  let timer

  const startTimerWithDuration = (duration) => {
    if (timer) timer = undefined
    secondsRemaining.value = duration

    timer = setInterval(() => {
      if (secondsRemaining.value > 0) {
        secondsRemaining.value -= 1
      }
    }, 1000)
  }

  const startTimerWithDate = (date) => {
    const secondsUntilDate = Math.floor((date - new Date()) / 1000)
    startTimerWithDuration(secondsUntilDate)
  }

  const startTimer = () => {
    if (date) {
      const cleanedDate = typeof date !== 'string' ? date : new Date(date)
      startTimerWithDate(cleanedDate)
    } else {
      startTimerWithDuration(duration)
    }
  }

  const percentageTimeRemaining = computed(() => (secondsRemaining.value / 60) * 100)

  return {
    secondsRemaining,
    percentageTimeRemaining,
    startTimer,
  }
}
