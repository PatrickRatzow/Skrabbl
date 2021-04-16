import ws from "../utils/signal-r"
import store from "../store/index"

class ChatService {
    sendMessage(message) {
        return ws.invoke("SendMessage", message)
    }

    async fetchMessages() {
        const { hasFetchedMessages } = store.state.user
        if (hasFetchedMessages) return false

        await ws.invoke("GetAllMessages")

        return true
    }
}

export default new ChatService()