import playerData from '@/data/players.json'

export const playerService = {
  async getPlayers() {
    try {
      return playerData.players
    } catch (error) {
      console.error('Error fetching players:', error)
      throw error
    }
  },
}
