import { useGameStore } from '@/stores/gameStore'
import { useRoundStore } from '@/stores/roundStore'

export const roundService = {
  async startRound() {
    try {
      const gameStore = useGameStore()
      gameStore.inGame = true
      gameStore.inRound = true
      gameStore.inLeaderboard = false
    } catch (error) {
      console.error('Error starting round:', error)
      throw error
    }
  },

  async startVoting() {
    try {
      const gameStore = useGameStore()
      gameStore.inVoting = true
    } catch (error) {
      console.error('Error ending round:', error)
      throw error
    }
  },

  async submitAnswer(answer) {
    try {
      const roundStore = useRoundStore()
      roundStore.hasReplied = true
      roundStore.promptContent = ''
      return answer
    } catch (error) {
      console.error('Error submitting answer:', error)
      throw error
    }
  },

  async submitVote(promptId) {
    try {
      const roundStore = useRoundStore()
      roundStore.hasVoted = true
    } catch (error) {
      console.error('Error submitting vote:', error)
      throw error
    }
  },

  async endRound() {
    try {
      const gameStore = useGameStore()
      const roundStore = useRoundStore()

      gameStore.inVoting = false
      gameStore.inRound = false
      roundStore.roundNumber++

      if (roundStore.roundNumber > roundStore.roundMax) {
        gameStore.inGameOver = true
        gameStore.inGame = true
      } else {
        gameStore.inLeaderboard = true
      }
    } catch (error) {
      console.error('Error ending round:', error)
      throw error
    }
  },
}
