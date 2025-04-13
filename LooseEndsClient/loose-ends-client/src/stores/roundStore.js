import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRoundStore = defineStore('round', () => {
  // Round state
  const hasReplied = ref(false) // Player has replied to the prompt
  const hasVoted = ref(false) // Player has voted for a reply
  const roundMax = 3 // Max number of rounds
  const roundNumber = ref(1) // Current round number
  const promptTime = ref(5) // Time limit for each round
  const voteTime = ref(5) // Time limit for voting

  const promptContent = ref('What would you do?')

  return {
    hasReplied,
    hasVoted,
    roundNumber,
    roundMax,
    promptTime,
    voteTime,

    promptContent,
  }
})
