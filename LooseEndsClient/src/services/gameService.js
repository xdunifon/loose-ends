import apiClient from '@/services/api'
import { useAuthStore } from '@/stores/authStore'
import { useGameStore } from '@/stores/gameStore'

export const gameService = {
  // Get data for entire session
  async getAsync() {
    const response = (await apiClient.get()).data
    const gameStore = useGameStore()
    gameStore.setState(response)
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

  // Start the game
  async startAsync() {
    await apiClient.post('start', {})
  },

  // Move the game into the next state
  async nextAsync() {
    await apiClient.post('next')
  },

  // Join an existing game using the game code and player's name
  async joinAsync(newGameCode, playerName) {
    const response = (await apiClient.post('join', { gameCode: newGameCode, name: playerName }))
      .data

    const authStore = useAuthStore()
    authStore.setToken(response.token)

    const gameStore = useGameStore()
    gameStore.gameCode = newGameCode
    gameStore.userId = response.playerId
  },

  // Answer a prompt using the existing response ID and the player's answer
  async answerAsync(responseId, answer) {
    await apiClient.post('answer', { responseId, answer })
  },

  // Vote for a response by its ID
  async voteAsync(responseId) {
    await apiClient.post('vote', { responseId })
  },
}
