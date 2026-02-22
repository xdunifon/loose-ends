<script setup>
//#region IMPORTS
import DebugBar from '@/pages/DebugBar.vue'

// Shared
import SharedLoading from '@/pages/SharedLoading.vue'

// Game pages
import GameLobby from '@/pages/game/GameLobby.vue'
import GameRound from '@/pages/game/GamePrompting.vue'
import GameVoting from '@/pages/game/GameVoting.vue'
import GameLeaderboard from '@/pages/game/GameLeaderboard.vue'
import GameWin from '@/pages/game/GameWin.vue'

// User pages
import UserDefault from '@/pages/user/UserSplash.vue'
import UserWelcome from '@/pages/user/UserWelcome.vue'
import UserPrompt from '@/pages/user/UserPrompt.vue'
import UserVoting from '@/pages/user/UserVoting.vue'

import { useGameStore } from '@/stores/gameStore.js'
import { signalRService } from '@/services/signalRService'
import { ref, onMounted } from 'vue'
//#endregion

const gameStore = useGameStore()

const promptContent = ref('')
const hasReplied = ref(false)
const roundEndTimestamp = ref()

const hasVoted = ref(false)
const votingEndTimestamp = ref()
const replies = ref()

onMounted(() => {
  signalRService.on('GameStarted', (roundDto) => {
    roundEndTimestamp.value = roundDto.EndDateTime
    const roundPrompt = roundDto.Prompts.Filter(p => p.AssignedPlayers.includes(gameStore.playerName))
    promptContent.value = roundPrompt
  })
});

</script>

<template>
  <DebugBar v-if="gameStore.debugMode" />

  <!-- Shared Loading takes over all when true -->
  <SharedLoading v-if="gameStore.loading" />

  <!-- Host Pages -->
  <div v-else-if="gameStore.isHost">
    <GameWin v-if="gameStore.inGameOver" />
    <GameLeaderboard v-else-if="gameStore.inLeaderboard" />
    <GameVoting v-else-if="gameStore.inVoting" :vote-end-time-stamp="votingEndTimestamp" />
    <GameRound v-else-if="gameStore.inRound" :round-end-time-stamp="roundEndTimestamp" />
    <GameLobby v-else />
  </div>

  <!-- Player Pages -->
  <div v-else>
    <UserVoting v-if="gameStore.inVoting" :has-voted="hasVoted" :voting-end-time-stamp="votingEndTimestamp"
      :replies="replies" />
    <UserPrompt v-else-if="gameStore.inRound" :has-replied="hasReplied" :round-end-timestamp="roundEndTimestamp"
      :prompt="prompt" />
    <UserWelcome v-else-if="!gameStore.inLobby" />
    <UserDefault v-else></UserDefault>
  </div>
</template>
