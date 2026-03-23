<script setup>
import { gameService } from '@/services/gameService'
import { useGameStore } from '@/stores/gameStore'
import { Button, InputText } from 'primevue'
import { ref } from 'vue'

const gameStore = useGameStore()

const answerInput = ref('')
const answer = async () => {
  if (!gameStore.playerResponse) return
  console.log(gameStore.playerResponse)
  await gameService.answerAsync(gameStore.playerResponse.responseId, answerInput.value)
}
</script>

<template>
  <div>
    <div>
      <p>PlayerPage</p>
      <p>{{ gameStore.gameCode }}</p>
    </div>
    <div v-if="gameStore.activeRound">
      <!-- PROMPTING -->
      <div v-if="gameStore.activeRound.answerDueUtc && !gameStore.activeRound.promptingCompleted">
        <div v-if="gameStore.playerPrompt && !gameStore.playerPrompt.answer">
          <div>{{ gameStore.playerPrompt.prompt }}</div>
          <InputText
            class="w-full"
            v-model="answerInput"
            label="Answer"
            placeholder="Enter your answer here"
          />
          <Button label="Submit" @click="answer" />
        </div>
      </div>
    </div>
  </div>
</template>
