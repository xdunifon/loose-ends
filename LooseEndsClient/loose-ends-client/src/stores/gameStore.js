import { ref } from 'vue'
import { defineStore } from 'pinia'
import { signalRService } from '@/services/signalRService'

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

  // Round states
  const roundMax = 3 // Max number of rounds
  const roundNumber = ref(1) // Current round number
  const promptTime = ref(5) // Time limit for each round
  const voteTime = ref(5) // Time limit for voting

  // Game data
  const sessionId = ref('')
  const playerId = ref('')
  const playerName = ref('')

  const players = ref([])

  const initSignalR = async () => {
    await signalRService.start()

    signalRService.on('GameStarted', (newRound) => {
      inGame.value = true
      inRound.value = true
      console.log(newRound)
    })

    signalRService.on('PlayerJoined', (newSessionId, newPlayerName) => {
      players.value.push(playerName)
      inLobby.value = true
      sessionId.value = newSessionId
      playerName.value = newPlayerName
    })

    signalRService.on('GameCreated', (newSessionId) => {
      sessionId.value = newSessionId
      isHost.value = true
      inLobby.value = true
      console.log(`Session ID ${newSessionId} received`)
    })

    signalRService.on('GameStarted', () => {
      inGame.value = true
      inLobby.value = true
      inRound.value = true
    })

    signalRService.on('RoundStarted', () => {
      inRound.value = true
    })

    // Add more listeners
  }

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

    initSignalR,
  }
})
