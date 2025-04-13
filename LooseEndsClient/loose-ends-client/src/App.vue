<script setup>
//#region IMPORTS
import DebugBar from '@/pages/DebugBar.vue'

// Shared
import SharedLoading from '@/pages/SharedLoading.vue'

// Game pages
import GameLobby from '@/pages/game/GameLobby.vue'
import GameRound from '@/pages/game/GameRound.vue'
import GameVoting from '@/pages/game/GameVoting.vue'
import GameLeaderboard from '@/pages/game/GameLeaderboard.vue'
import GameWin from '@/pages/game/GameWin.vue'

// User pages
import UserDefault from '@/pages/user/UserDefault.vue'
import UserWelcome from '@/pages/user/UserWelcome.vue'
import UserPrompt from '@/pages/user/UserPrompt.vue'
import UserVoting from '@/pages/user/UserVoting.vue'

import { useGameStore } from '@/stores/gameStore.js'
//#endregion

const gameStore = useGameStore()
</script>

<template>
  <DebugBar v-if="gameStore.debugMode" />

  <!-- Shared Loading takes over all when true -->
  <SharedLoading v-if="gameStore.loading" />

  <!-- Host Pages -->
  <div v-else-if="gameStore.isHost">
    <GameWin v-if="gameStore.inGameOver" />
    <GameLeaderboard v-else-if="gameStore.inLeaderboard" />
    <GameVoting v-else-if="gameStore.inVoting" />
    <GameRound v-else-if="gameStore.inRound" />
    <GameLobby v-else />
  </div>

  <!-- Player Pages -->
  <div v-else>
    <UserVoting v-if="gameStore.inVoting" />
    <UserPrompt v-else-if="gameStore.inRound" />
    <UserWelcome v-else-if="!gameStore.inLobby" />
    <UserDefault v-else></UserDefault>
  </div>
</template>
