import ws from "../../signal-r"

const state = () => ({
    username: "",
    jwt: ""
})

const actions = {
    login({ commit }, obj) {
        commit("setUser", obj)
    },
    logout({ commit }) {
        commit("removeUser")
    }
}

const mutations = {
    setUser(state, { username, jwt }) {
        state.username = username,
        state.jwt = jwt
    },
    removeUser(state) {
        state.username = ""
        state.jwt = ""
    }
}

const getters = {
    isLoggedIn: state => state.jwt.length > 0
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
    ws.on("roundStart", () => { });

}