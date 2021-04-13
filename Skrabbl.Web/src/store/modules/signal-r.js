const state = () => ({
    connected: false
})

const getters = {
    isConnected: state => state.connected
}

const actions = {
    connectionOpened({commit}) {
        commit("setConnected", true)
    },
    connectionClosed({commit}) {
        commit("setConnected", false)
    }
}

const mutations = {
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