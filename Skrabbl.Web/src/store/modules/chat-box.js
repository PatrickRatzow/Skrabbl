import ws from "../../signal-r"

const gameId = 3

const state = () => ({
    messages: [],
    status: ""
})

const getters = {}

const actions = {
    sendMessage({commit}, message) {
        commit("sendMessage", message)
    },
    addMessage({commit}, object) {
        commit("addMessage", object)
    },
    resetMessages({commit}) {
        commit("removeMessages")
    },
    connectionOpened({commit}) {
        commit("setConnected", true)
    },
    connectionClosed({commit}) {
        commit("setConnected", false)
    },
    async fetchMessages({commit, state}) {
        if (state.messages.length)
            return

        ws.invoke("GetAllMessages", gameId)
    }
}

const mutations = {
    sendMessage(state, message) {
        const userId = 25 // TODO: Replace this later
        ws.invoke("SendMessage", gameId, userId, message)
    },
    addMessage(state, msg) {
        state.messages.push(msg)
    },
    removeMessages(state) {
        state.messages = [];
    },
    setConnected(state, status) {
        state.connected = status
    }
}

const store = {
    namespaced: true,
    state,
    actions,
    getters,
    mutations
}

export default store

export function setupSignalR(ws, store) {
    ws.on("ReceiveMessage", (user, message) => {
        store.dispatch("chatBox/addMessage", {
            action: "chatMessage",
            user,
            message
        })
    });
    ws.on("GuessedWord", (user) => {
        store.dispatch("chatBox/addMessage", {
            action: "guessedWord",
            user
        })
    })
}