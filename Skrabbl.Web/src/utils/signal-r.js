import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

class SignalRConnection {
    constructor() {
        this.connected = false
        this.queues = {
            invoke: [],
            on: []
        }
        this.observers = []
    }

    invoke(id, ...args) {
        if (!this.connected) {
            this.queues.invoke.push({
                id,
                args: [...args]
            })

            return
        }

        this.conn.invoke(id, ...args)
    }

    on(id, callback) {
        if (!this.connected) {
            this.queues.on.push({
                id,
                callback
            })

            return
        }

        this.conn.on(id, callback)
    }

    async start(token) {
        if (!this.conn) {
            this.conn = new HubConnectionBuilder()
                .withUrl("/ws/game", { accessTokenFactory: () => token })
                .configureLogging(LogLevel.Information)
                .build()
        }

        await this.conn.start()

        this.connected = true
        this.observers.forEach(cb => cb(this.isConnected))
        this.queues.on.forEach(entry => this.conn.on(entry.id, entry.callback));
        this.queues.on = [];
        this.queues.invoke.forEach(entry => this.conn.invoke(entry.id, ...entry.args));
        this.queues.invoke = [];
    }

    stop() {
        this.connected = false

        return this.conn.stop()
    }
}


export default new SignalRConnection()

