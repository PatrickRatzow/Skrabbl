import ws from "../../signal-r"
import {setupSignalR as chatBoxSetupSignalR} from "../modules/chat-box"

export default function createSignalRPlugin() {
    return async store => {
        chatBoxSetupSignalR(ws, store)

        try {
            await ws.start()

            store.dispatch("signalR/connectionOpened")
        } catch {
            store.dispatch("signalR/connectionClosed")
        }
    }
}