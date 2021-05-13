import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import store from "../store/index"

class SignalRConnection {
    constructor() {
        this.conn = new HubConnectionBuilder()
            .withUrl("/ws/game", {
                accessTokenFactory: this.getAccessToken
            })
            .configureLogging(LogLevel.Information)
            .build()

        this.connected = false
        this.queues = {
            invoke: [],
            on: []
        }
        this.observers = []
    }

    async getAccessToken() {
        const auth = store.state.user.auth;

        if (auth === null) {
            return;
        }

        const expiresAt = auth.jwt.expiresAt
        const currentTime = new Date()
        const hasExpired = currentTime > expiresAt

        if (hasExpired) {
            await store.dispatch("user/refreshLogin", auth.refreshToken.token)
        }

        return auth.jwt?.token;
    }

    invoke(id, ...args) {
        if (!this.connected) {
            this.queues.invoke.push({
                id,
                args: [...args]
            })

            return Promise.resolve()
        }

        this.conn.invoke(id, ...args)
    }

    on(id, callback) {
        if (!this.connected) {
            this.queues.on.push({
                id,
                callback
            })

            return Promise.resolve()
        }

        this.conn.on(id, callback)
    }

    async start() {
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

