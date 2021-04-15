import ws from "../../signal-r"

const state = () => ({
    username: "",
    jwt: ""
})

const actions = {
    login({ commit }, obj) {
        commit("setUser", obj)
        localStorage.setItem("user", JSON.stringify(obj))
    },
    logout({ commit }) {
        commit("removeUser")
        localStorage.removeItem("user")
    },
    loadUser({ commit }) {
        const user = localStorage.getItem("user")
        if (user === null) return

        commit("setUser", JSON.parse(user))
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