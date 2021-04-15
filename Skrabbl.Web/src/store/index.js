import Vue from "vue"
import Vuex from "vuex"
import chat from "./modules/chat"
import signalR from "./modules/signal-r"
import canvas from "./modules/canvas"
import game from "./modules/game"
import user from "./modules/user"
import createSignalRPlugin from "./plugins/signal-r"

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        signalR,
        chat,
        canvas,
        game,
        user
    },
    plugins: [
        createSignalRPlugin()
    ]
})