import { ref } from 'vue'
import { defineStore } from 'pinia'
import { signalRService } from '@/services/signalRService'

export const useGameStore = defineStore('game', () => {

  // Game data
  const gameCode = ref('')
  const playerId = ref('')
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
  }

  return {
    gameCode,
    playerId,
    players,

    initSignalR,
  }
})
