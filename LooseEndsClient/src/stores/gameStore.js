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

    signalRService.on(events.gameOver, (dto) => {
      console.log(events.gameOver, dto)

      // Mark final round completed and end entire game
      // Show final leaderboard + player winner
    })

    signalRService.on(events.roundStarted, (dto) => {
      console.log(events.roundStarted, dto)

      // get round using dto.roundNumber
      // set answer due date on round
    })

    signalRService.on(events.promptingEnded, (dto) => {
      console.log(events.promptingEnded, dto)

      // mark prompting completed for current round
      // Or stop somehow
    })

    signalRService.on(events.votingStarted, (dto) => {
      console.log(events.votingStarted, dto)

      // get current round and prompmt id
      // sest vote due date
      // present options to player
    })

    signalRService.on(events.votingEnded, (dto) => {
      console.log(events.votingEnded, dto)

      // mark prompt voting over
      // stop voting and display results
    })

    signalRService.on(events.roundEnded, (dto) => {
      console.log(events.roundEnded, dto)

      // Mark round ended
      // Display leaderboard results
    })

    if (isHost.value) {
      signalRService.on(events.playerJoined, (dto) => {
        console.log(events.playerJoined, dto)

        players.value.push(dto)
      })

      signalRService.on(events.playerSubmitted, (playerId) => {
        console.log(events.playerSubmitted, playerId)

        // get active round, find prompt with plaerId, set submitted?
        // If all players submitted, send out next()
      })

      signalRService.on(events.playerVoted, (playerId) => {
        console.log(events.playerVoted, playerId)
        
        // get active voting prompt, find player with this id, set submitted?
        // If all players submitted, send out next()
      })
    }
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
