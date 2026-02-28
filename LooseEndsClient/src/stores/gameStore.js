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
    rounds: [],
  })

  const setState = (getRes) => {
    gameCode.value = getRes.gameCode
    isHost.value = getRes.isHost
    userId.value = getRes.userId
    players.value = getRes.players
    gameState.value = {
      promptingDuration: getRes.promptingDuration,
      votingDuration: getRes.votingDuration,
      rounds: getRes.rounds,
    }
  }

  const initSignalR = async () => {
    await signalRService.startAsync()
    await signalRService.sendAsync(events.joinSession)

    signalRService.on(events.gameStarted, (dto) => {
      console.log(events.gameStarted, dto)

      gameCode.value = dto.gameCode
      players.value = dto.players
      gameState.value = {
        promptingDuration: dto.promptingDuration,
        votingDuration: dto.votingDuration,
        rounds: dto.rounds,
      }
    })

    signalRService.on(events.playerJoined, (dto) => {
      console.log(events.playerJoined, dto)

      players.value.push(dto)
    })
  }

  return {
    gameCode,
    gameState,
    userId,
    players,
    isHost,

    setState,
    initSignalR,
  }
})
