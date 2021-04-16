import ws from "../../utils/signal-r"

const gameId = 3

const state = () => ({
    messages: [],
    hasFetchedMessages: false
})

const getters = {}

const actions = {
    sendMessage({ commit }, message) {
        commit("sendMessage", message)
    },
    addMessage({ commit }, object) {
        commit("addMessage", object)
    },
    resetMessages({ commit }) {
        commit("removeMessages")
    },
    connectionOpened({ commit }) {
        commit("setConnected", true)
    },
    connectionClosed({ commit }) {
        commit("setConnected", false)
    },
    fetchMessages({ commit, state }) {
        if (state.hasFetchedMessages)
            return

        commit("fetchMessages")
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
    },
    fetchMessages(state) {
        state.hasFetchedMessages = true

        ws.invoke("GetAllMessages", gameId)
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
        store.dispatch("chat/addMessage", {
            action: "chatMessage",
            user,
            message
        })
    });
    ws.on("GuessedWord", (user) => {
        store.dispatch("chat/addMessage", {
            action: "guessedWord",
            user
        })
    })
}