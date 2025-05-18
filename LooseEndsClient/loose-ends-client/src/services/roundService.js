export const roundService = {
  async submitAnswer(answer) {
    // signalR to submit answer
  },

  async submitVote(promptId) {
    // signalR to submit prompt
  },

  async endVoting() {
    // signalR to end voting / progress to next step in game cycle
  },

  async endPrompting() {
    // signalR to end prompting / progress to next step in game cycle
  },
}
