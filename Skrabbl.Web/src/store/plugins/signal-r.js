import ws from "../../utils/signal-r"
import { setupSignalR as chatSetupSignalR } from "../modules/chat"
import { setupSignalR as gameSetupSignalR } from "../modules/game"
import { setupSignalR as userSetupSignalR } from "../modules/user"

export default function createSignalRPlugin() {
    return async store => {
        store.dispatch("signalR/setConnection", ws)

        chatSetupSignalR(ws, store)
        gameSetupSignalR(ws, store)
        userSetupSignalR(ws, store)
    }
}