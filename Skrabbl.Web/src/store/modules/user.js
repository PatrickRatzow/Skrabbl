import UserService from "../../services/user.service"

const state = () => ({
    auth: null,
    showLoginModal: false,
    showRegisterModal: false,
    showLogoutModal: false,
    userId: 25,
    gameId: 3
})

const actions = {
    async login({ commit, dispatch }, { username, password, rememberMe }) {
        await UserService.login(username, password, rememberMe)

        const auth = UserService.getAuthInfoFromCache()
        commit("setAuth", auth)
        commit("setLoginModalVisible", false)

        await dispatch("signalR/connect", null, { root: true })
    },
    async logout({ commit, dispatch }) {
        UserService.logout()

        commit("removeAuth")
        commit("setLogoutModalVisible", false)

        await dispatch("signalR/disconnect", {}, { root: true })
    },
    async refreshLogin({ commit }, token) {
        await UserService.refreshToken(token)

        const auth = UserService.getAuthInfoFromCache()
        commit("setAuth", auth)
    },
    loadUser({ commit, dispatch }) {
        const auth = UserService.getAuthInfoFromCache()
        if (!auth) return

        commit("setAuth", auth)
        dispatch("signalR/connect", null, { root: true })
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
    setAuth(state, auth) {
        state.auth = auth
    },
    removeAuth(state) {
        state.auth = null
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
    isLoggedIn: state => state.auth !== null,
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