import { useGameStore } from '@/stores/gameStore'
import { signalRService } from './signalRService'

export const gameService = {
  async joinGame(sessionId, playerName) {
    try {
      if (sessionId === '' || playerName === '') {
        alert('Please enter both game code and player name.')
        return
      }
      const gameStore = useGameStore()
      await gameStore.initSignalR()
      signalRService.send('JoinGame', sessionId, playerName)
    } catch (error) {
      console.error('Error joining game:', error)
      throw error
    }
  },

  async createGame() {
    const gameStore = useGameStore()
    await gameStore.initSignalR()
    await signalRService.send('CreateGame')
  },

  async startGame(sessionId) {
    try {
      await signalRService.send('StartGame', sessionId)
    } catch (error) {
      console.error('Error starting game:', error)
      throw error
    }
  },
}
