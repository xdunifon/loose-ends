<script setup>
//#region IMPORTS
import { ref } from 'vue'
import { FloatLabel, InputText, Button } from 'primevue'
import { useGameStore } from '@/stores/gameStore.js'
import { gameService } from '@/services/gameService'
//#endregion

//#region VARIABLES
const gameStore = useGameStore()
const gameCode = ref('')
const playerName = ref('')

//#region FUNCTIONS
const joinGame = async () => await gameService.joinGame(gameCode.value, playerName.value)
const createGame = async () => await gameService.createGame();
//#endregion
</script>

<template>
  <div v-if="!gameStore.inLobby">
    <h1 class="text-3xl font-bold text-center">LOOSE ENDS</h1>

    <FloatLabel variant="in" class="mt-10">
      <InputText v-model="gameCode" class="w-full" />
      <label>Enter Game Code</label>
    </FloatLabel>

    <FloatLabel variant="in" class="mt-3">
      <InputText v-model="playerName" class="w-full" />
      <label>Enter Your Name</label>
    </FloatLabel>

    <Button label="Join Game" class="mt-5 w-full" @click="joinGame"></Button>
    <Button label="Create Game" class="mt-5 w-full" @click="createGame"></Button>
  </div>
  <div v-else>
    <h1 class="text-3xl font-bold text-center">Welcome to the Game!</h1>
    <p class="text-center mt-2">You are now in the lobby. Waiting for the host to start the game...</p>
  </div>

</template>
