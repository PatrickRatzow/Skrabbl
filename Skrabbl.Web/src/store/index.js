import Vue from "vue"
import Vuex from "vuex"
import chatBox from "./modules/chat-box"
import signalR from "./modules/signal-r"
import createSignalRPlugin from "./plugins/signal-r"

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        signalR,
        chatBox
    },
    plugins: [
        createSignalRPlugin()
    ]
})