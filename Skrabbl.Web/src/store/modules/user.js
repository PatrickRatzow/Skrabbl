const state = () => ({
    username: "",
    jwt: "",
    showLoginModal: false,
    showRegisterModal: false,
    showLogoutModal: false,
})

const actions = {
    login({ commit }, obj) {
        commit("setUser", obj)
        commit("setLoginModalVisible", false)

        localStorage.setItem("user", JSON.stringify(obj))
    },
    logout({ commit }) {
        commit("removeUser")
        commit("setLogoutModalVisible", false)

        localStorage.removeItem("user")
    },
    loadUser({ commit }) {
        const user = localStorage.getItem("user")
        if (user === null) return

        commit("setUser", JSON.parse(user))
    },
    setLoginModalVisible({ commit }, visible) {
        commit("setLoginModalVisible", visible)
    },
    setRegisterModalVisible({ commit }, visible) {
        commit("setRegisterModalVisible", visible)
    },
    setLogoutModalVisible({ commit }, visible) {
        commit("setLogoutModalVisible", visible)
    },
}

const mutations = {
    setUser(state, { username, jwt }) {
        state.username = username
        state.jwt = jwt
    },
    removeUser(state) {
        state.username = ""
        state.jwt = ""
    },
    setLoginModalVisible(state, visible) {
        state.showLoginModal = visible
    },
    setRegisterModalVisible(state, visible) {
        state.showRegisterModal = visible
    },
    setLogoutModalVisible(state, visible) {
        state.showLogoutModal = visible
    }
}

const getters = {
    isLoggedIn: state => state.jwt.length > 0,
    isLoginModalVisible: state => state.showLoginModal && !getters.isLoggedIn(state),
    isRegisterModalVisible: state => state.showRegisterModal && !getters.isLoggedIn(state),
    isLogoutModalVisible: state => state.showLogoutModal && getters.isLoggedIn(state)
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