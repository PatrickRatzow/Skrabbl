import ws from "../../signal-r"

const state = () => ({
    username: "",
    jwt: ""
})

const actions = {
    login({ commit }, obj) {
        commit("login", obj)
    }
}

const mutations = {
    login(state, { username, jwt }) {
        state.username = username,
        state.jwt = jwt
    }
}

const getters = {}

const store = {
    namespaced: true,
    state,
    actions,
    getters,
    mutations
}

export default store

export function setupSignalR(ws, store) {
    ws.on("roundStart", () => { });

}