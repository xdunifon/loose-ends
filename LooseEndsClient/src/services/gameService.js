import apiClient from '@/services/api'
import { useAuthStore } from '@/stores/authStore'
import { useGameStore } from '@/stores/gameStore'

export const gameService = {
  // Get data for entire session
  async getAsync() {
    const response = (await apiClient.get()).data

    const { players, gameState } = useGameStore()
    players.value = response.players
    gameState.value.promptingDuration = response.promptingDuration
    gameState.value.votingDuration = response.votingDuration
    gameState.value.rounds = response.rounds
  },

  // Create a new game
  async createAsync() {
    const response = (await apiClient.post('create')).data

    const authStore = useAuthStore()
    authStore.setToken(response.token)

    const gameStore = useGameStore()
    gameStore.isHost = true
    gameStore.userId = response.hostId
    gameStore.gameCode = response.gameCode
  },

  // Join an existing game
  async joinAsync(newGameCode, playerName) {
    const response = (await apiClient.post('join', { gameCode: newGameCode, name: playerName }))
      .data

    const authStore = useAuthStore()
    authStore.setToken(response.token)

    const gameStore = useGameStore()
    gameStore.gameCode = newGameCode
    gameStore.userId = response.playerId
  },
}
