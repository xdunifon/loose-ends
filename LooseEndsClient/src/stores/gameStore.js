import { ref } from 'vue'
import { defineStore } from 'pinia'
import { signalRService } from '@/services/signalRService'
import { events } from '@/services/signalREvents'

export const useGameStore = defineStore('game', () => {
  const gameCode = ref('')
  const userId = ref('')
  const players = ref([])
  const isHost = ref(false)

  const gameState = ref({
    promptingDuration: 0,
    votingDuration: 0,
    rounds: []
  })

  const initSignalR = async () => {
    await signalRService.start()

    signalRService.on(events.gameStarted, (dto) => {
      console.log(events.gameStarted, dto)

      gameCode.value = dto.gameCode
      players.value = dto.players
      gameState.value = {
        promptingDuration: dto.promptingDuration,
        votingDuration: dto.votingDuration,
        rounds: dto.rounds
      }
    })
  }

  return {
    gameCode,
    userId,
    players,

    initSignalR,
  }
})
