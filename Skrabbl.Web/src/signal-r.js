import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";

const SignalRConnection = (function () {
    function SignalRConnection() {
        this.conn = new HubConnectionBuilder()
            .withUrl("/ws/game")
            .configureLogging(LogLevel.Information)
            .build()

        this.connected = false
        this.queue = []
        this.observers = []
    }

    SignalRConnection.prototype.invoke = function (id, ...args) {
        if (!this.connected) {
            this.queue.push({
                id,
                args: [...args]
            })

            return
        }

        this.conn.invoke(id, ...args)
    }

    SignalRConnection.prototype.on = function (id, callback) {
        this.conn.on(id, callback)
    }

    SignalRConnection.prototype.start = function () {
        return this.conn.start()
            .then(() => {
                this.connected = true
                this.observers.forEach(cb => cb(this.isConnected))

                this.queue.forEach(entry => {
                    this.conn.invoke(entry.id, ...entry.args)
                })
            })
            .catch(console.error)
    }

    return SignalRConnection
}())

export default new SignalRConnection()

