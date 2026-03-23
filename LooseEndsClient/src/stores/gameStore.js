import { computed, ref } from 'vue'
import { defineStore } from 'pinia'
import { signalRService } from '@/services/signalRService'
import { events } from '@/services/signalREvents'
import { gameService } from '@/services/gameService'

export const useGameStore = defineStore('game', () => {
  const gameCode = ref('')
  const userId = ref('')
  const players = ref([])
  const isHost = ref(false)

  const gameStarted = computed(() => gameState.value.rounds.some((r) => r.answerDueDate !== null))

  const activeRound = computed(() => {
    const sorted = [...gameState.value.rounds].sort((a, b) => a.number - b.number)
    return sorted.find((r) => r.answerDueUtc && !r.votingCompleted) || null
  })

  const playerPrompt = computed(() => {
    if (!activeRound.value) return null

    return activeRound.value.prompts.find((p) =>
      p.voteOptions.some((v) => v.playerId === userId.value),
    )
  })

  const playerResponse = computed(() => {
    if (!playerPrompt.value) return null

    return playerPrompt.value.voteOptions.find((v) => v.playerId === userId.value)
  })

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

    signalRService.on(events.gameStarted, async (dto) => {
      console.log(events.gameStarted, dto)

      gameCode.value = dto.gameCode
      players.value = dto.players
      gameState.value = {
        promptingDuration: dto.promptingDuration,
        votingDuration: dto.votingDuration,
        rounds: dto.rounds,
      }

      if (isHost.value) {
        await gameService.nextAsync()
      }
    })

    signalRService.on(events.gameOver, (dto) => {
      console.log(events.gameOver, dto)

      // Mark final round completed and end entire game
      // Show final leaderboard + player winner
    })

    signalRService.on(events.roundStarted, (dto) => {
      console.log(events.roundStarted, dto)

      const round = gameState.value.rounds.find((r) => r.number === dto.number)
      round.answerDueUtc = dto.endsAt
    })

    signalRService.on(events.promptingEnded, (dto) => {
      console.log(events.promptingEnded, dto)

      if (activeRound.value) {
        activeRound.value.promptingCompleted = true
      }
    })

    signalRService.on(events.votingStarted, (dto) => {
      console.log(events.votingStarted, dto)

      if (activeRound.value) {
        activeRound.value.activeVotingPromptId = dto.promptId
        const prompt = activeRound.value.prompts.find((p) => p.id == dto.promptId)
        prompt.voteDueUtc = dto.voteDueUtc
        prompt.voteOptions = dto.voteOptions
      }
    })

    signalRService.on(events.votingEnded, (dto) => {
      console.log(events.votingEnded, dto)

      activeRound.value.activeVotingPromptId = null
      activeRound.value.votingCompleted = true
    })

    signalRService.on(events.roundEnded, (dto) => {
      console.log(events.roundEnded, dto)

      // Make change to show leaderboard?
    })

    if (isHost.value) {
      signalRService.on(events.playerJoined, (dto) => {
        console.log(events.playerJoined, dto)

        players.value.push(dto)
      })

      signalRService.on(events.playerSubmitted, (playerId) => {
        console.log(events.playerSubmitted, playerId)

        if (activeRound.value) {
          activeRound.value
        }
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

    activeRound,
    gameStarted,
    playerPrompt,
    playerResponse,

    setState,
    initSignalR,
  }
})
