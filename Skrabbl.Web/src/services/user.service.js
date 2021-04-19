import axios from "../utils/axios"

class UserService {
    async login(username, password, rememberMe) {
        const { data } = await axios.post("user/login", {
            username,
            password
        })

        this.storeLogin(data, rememberMe)
    }

    async refreshToken(refreshToken) {
        const { data } = await axios.post("user/refresh", {
            token: refreshToken
        })

        this.storeLogin(data)
    }

    storeLogin(data, rememberMe) {
        let storage
        if (rememberMe !== undefined) {
            storage = rememberMe ? localStorage : sessionStorage
        } else {
            storage = localStorage.getItem("auth") !== null ? localStorage : sessionStorage
        }

        storage.setItem("auth", JSON.stringify(data))
    }

    logout() {
        const auth = this.getAuthInfoFromCache()
        this.clearAuthCache()

        return axios.post("user/logout", {
            token: auth?.refreshToken?.token
        })
    }

    clearAuthCache() {
        localStorage.removeItem("auth")
        sessionStorage.removeItem("auth")
    }

    getAuthInfoFromCache() {
        let auth = localStorage.getItem("auth")
        if (auth === null)
            auth = sessionStorage.getItem("auth")
        if (auth === null)
            return

        const json = JSON.parse(auth)
        if (json?.jwtToken?.expiresAt) {
            json.jwtToken.expiresAt = new Date(json.jwtToken.expiresAt)
        }
        if (json?.refreshToken?.expiresAt) {
            json.refreshToken.expiresAt = new Date(json.refreshToken.expiresAt)
        }

        return json
    }
}

export default new UserService()
