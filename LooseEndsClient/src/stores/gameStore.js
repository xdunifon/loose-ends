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
      players.value.push(newPlayerName)
      inLobby.value = true

      if (!isHost.value) {
        sessionId.value = newSessionId
        playerName.value = newPlayerName
      }
    })

    signalRService.on('GameCreated', (newSessionId) => {
      sessionId.value = newSessionId
      isHost.value = true
      inLobby.value = true
      console.log(`Session ID ${newSessionId} received`)
    })

    signalRService.on('GameStarted', (gameRound) => {
      inGame.value = true
      inLobby.value = true
      inRound.value = true
    })

    signalRService.on('RoundStarted', () => {
      inRound.value = true
    })

    signalRService.on('VotingEnded', () => {
      inVoting.value = false
    })
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
