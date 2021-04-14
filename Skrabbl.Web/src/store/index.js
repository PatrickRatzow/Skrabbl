import Vue from "vue"
import Vuex from "vuex"
import chat from "./modules/chat"
import signalR from "./modules/signal-r"
import createSignalRPlugin from "./plugins/signal-r"

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        signalR,
        chat
    },
    plugins: [
        createSignalRPlugin()
    ]
})