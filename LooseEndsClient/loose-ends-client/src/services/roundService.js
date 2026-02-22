import { signalRService } from '@/services/signalRService'
import { useGameStore } from '@/stores/gameStore'

export const roundService = {
  async submitAnswer(answer) {
    const gameStore = useGameStore()
    signalRService.send('PlayerVote', gameStore.sessionId, gameStore.playerName, answer)
  },

  async submitVote(promptId) {
    // signalR to submit prompt
  },

  async endVoting() {
    // signalR to end voting / progress to next step in game cycle
  },

  async endPrompting() {
    // signalR to end prompting / progress to next step in game cycle
  },
}
