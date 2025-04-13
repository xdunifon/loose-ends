import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useGameStore = defineStore('game', () => {
  // Programmer
  const debugMode = ref(true) // Debug mode for development

  // Game states
  const isHost = ref(false) // Host vs player
  const inLobby = ref(false) // Waiting for players
  const inGame = ref(false) // Game has started
  const inRound = ref(false) // Round has started, players are filling out prompts
  const inVoting = ref(false) // Voting phase of current round, players are voting
  const inLeaderboard = ref(false) // End of current round, leaderboard is shown
  const inGameOver = ref(false) // Game is over, show final leaderboard

  // Game data
  const sessionId = ref('')
  const playerId = ref('')
  const playerName = ref('')

  const players = ref([])

  return {
    debugMode,

    isHost,
    inLobby,
    inGame,
    inRound,
    inVoting,
    inLeaderboard,
    inGameOver,

    sessionId,
    playerId,
    playerName,
    players,
  }
})
