const state = () => ({
    username: "",
    jwt: "",
    showLoginModal: false,
    showRegisterModal: false,
    showLogoutModal: false,
    userId: 25,
    gameId: 3
})

const actions = {
    login({ commit, dispatch }, { username, jwt, rememberMe }) {
        const user = {
            username,
            jwt
        }
        commit("setUser", user)
        commit("setLoginModalVisible", false)

        const json = JSON.stringify(user);
        if (rememberMe) {
            localStorage.setItem("user", json);
        } else {
            sessionStorage.setItem("user", json);
        }

        dispatch("signalR/connect", jwt, { root: true })
    },
    logout({ commit, dispatch }) {
        commit("removeUser")
        commit("setLogoutModalVisible", false)

        sessionStorage.removeItem("user")
        localStorage.removeItem("user")

        dispatch("signalR/disconnect", {}, { root: true })
    },
    loadUser({ commit, dispatch }) {
        let user = localStorage.getItem("user")
        if (user === null) user = sessionStorage.getItem("user")
        if (user == null) return;

        const json = JSON.parse(user)
        commit("setUser", json)
        dispatch("signalR/connect", json.jwt, { root: true })
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