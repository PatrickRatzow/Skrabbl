import ws from "../utils/signal-r"

class ChatService {
    sendMessage(message) {
        return ws.invoke("SendMessage", message)
    }

    fetchMessages() {
        return ws.invoke("GetAllMessages")
    }
}

export default new ChatService()