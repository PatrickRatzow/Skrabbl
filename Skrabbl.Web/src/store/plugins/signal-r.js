import ws from "../../signal-r"
import {setupSignalR as chatSetupSignalR} from "../modules/chat"

export default function createSignalRPlugin() {
    return async store => {
        chatSetupSignalR(ws, store)

        try {
            await ws.start()

            store.dispatch("signalR/connectionOpened")
        } catch {
            store.dispatch("signalR/connectionClosed")
        }
    }
}