import { useGameStore } from '@/stores/gameStore'

export const gameService = {
  async getSessionId() {
    try {
      let sessionId = 5
      return sessionId
    } catch (error) {
      console.error('Error fetching session ID:', error)
      throw error
    }
  },

  async joinGame(sessionId, playerName) {
    try {
      if (sessionId === '' || playerName === '') {
        alert('Please enter both game code and player name.')
        return
      }
      const gameStore = useGameStore()
      gameStore.inLobby = true
      gameStore.gameCode = sessionId
      gameStore.playerName = playerName
    } catch (error) {
      console.error('Error joining game:', error)
      throw error
    }
  },

  async startGame(sessionId) {
    try {
      const gameStore = useGameStore()
      gameStore.gameCode = sessionId
      gameStore.inLobby = true
      gameStore.inGame = true
      gameStore.inRound = true
    } catch (error) {
      console.error('Error starting game:', error)
      throw error
    }
  },
}
