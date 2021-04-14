const state = () => ({
    connected: false,
    connection: null
})

const getters = {}

const actions = {
    setConnection({ commit }, connection) {
        commit("setConnection", connection)
    },
    connectionOpened({ commit }) {
        commit("setConnected", true)
    },
    connectionClosed({ commit }) {
        commit("setConnected", false)
    }
}

const mutations = {
    setConnected(state, status) {
        state.connected = status
    },
    setConnection(state, connection) {
        state.connection = connection
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