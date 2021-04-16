import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

class SignalRConnection {
    constructor() {
        this.conn = new HubConnectionBuilder()
            .withUrl("/ws/game")
            .configureLogging(LogLevel.Information)
            .build()

        this.connected = false
        this.queue = []
        this.observers = []
    }

    invoke(id, ...args) {
        if (!this.connected) {
            this.queue.push({
                id,
                args: [...args]
            })

            return
        }

        this.conn.invoke(id, ...args)
    }

    on(id, callback) {
        this.conn.on(id, callback)
    }

    start() {
        return this.conn.start()
            .then(() => {
                this.connected = true
                this.observers.forEach(cb => cb(this.isConnected))

                this.queue.forEach(entry => {
                    this.conn.invoke(entry.id, ...entry.args)
                })
            })
    }
}


export default new SignalRConnection()

