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
    },
    async connect({ state, dispatch }, token) {
        try {
            await state.connection.start(token)

            dispatch("connectionOpened")
        } catch {
            dispatch("connectionClosed")
        }
    },
    async disconnect({ state, dispatch }) {
        await state.connection.stop();

        dispatch("connectionClosed")
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