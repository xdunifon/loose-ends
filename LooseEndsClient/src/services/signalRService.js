import * as signalR from '@microsoft/signalr'

class SignalRService {
  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7029/gamehub')
      .withAutomaticReconnect()
      .build()

    this.started = false
  }

  async start() {
    if (this.started) return
    this.started = true

    try {
      await this.connection.start()
      console.log('SignalR connected')
    } catch (err) {
      console.error('SignalR connection error:', err)
      setTimeout(() => this.start(), 2000)
    }
  }

  on(event, callback) {
    this.connection.on(event, callback)
  }

  async send(method, ...args) {
    try {
      await this.connection.invoke(method, ...args)
    } catch (err) {
      console.error(`Error sending ${method}:`, err)
    }
  }
}

export const signalRService = new SignalRService()
