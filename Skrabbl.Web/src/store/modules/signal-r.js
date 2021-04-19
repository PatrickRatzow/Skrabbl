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
    async connect({ state, dispatch }) {
        try {
            await state.connection.start()

            await dispatch("connectionOpened")
        } catch {
            await dispatch("connectionClosed")
        }
    },
    async disconnect({ state, dispatch }) {
        await state.connection.stop();

        await dispatch("connectionClosed")
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