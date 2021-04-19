import ChatService from "../../services/chat.service"

const state = () => ({
    messages: [],
    hasFetchedMessages: false
})

const getters = {}

const actions = {
    async sendMessage({ commit }, message) {
        await ChatService.sendMessage(message)
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
    async fetchMessages({ commit, state }) {
        if (state.hasFetchedMessages) return

        commit("setFetchedMessages", true)
        await ChatService.fetchMessages()
    }
}

const mutations = {
    addMessage(state, msg) {
        state.messages.push(msg)
    },
    removeMessages(state) {
        state.messages = [];
    },
    setConnected(state, status) {
        state.connected = status
    },
    setFetchedMessages(state, hasFetched) {
        state.hasFetchedMessages = hasFetched
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