import store from "../store/index"
import axios from "axios";

let requestQueue = []

function addToRequestQueue(cfg) {
    return new Promise((resolve, reject) => {
        requestQueue.push({ url: cfg.url, resolve, reject })
    })
}

function refreshJWTTokenIfNeeded(cfg) {
    // Hack so our refresh request goes through
    if (cfg.url === "user/refresh") {
        return Promise.resolve()
    }

    if (requestQueue.length) {
        return addToRequestQueue(cfg)
    }

    const auth = store.state.user.auth
    if (auth !== null) {
        const expiresAt = auth.jwtToken.expiresAt
        const currentTime = new Date()
        const hasExpired = currentTime > expiresAt
        if (hasExpired) {
            addToRequestQueue(cfg)

            return store.dispatch("user/refreshLogin", auth.refreshToken.token)
                .then(resp => {
                    requestQueue.forEach(e => e.resolve(resp))
                })
                .catch(err => {
                    requestQueue.forEach(e => e.reject(err))
                }).finally(() => {
                    requestQueue = []
                })
        }
    }

    return Promise.resolve()
}

function authorizationToken() {
    if (store.state.user.auth !== null) {
        return { Authorization: `Bearer ${store.state.user.auth.jwtToken.token}` }
    } else {
        return {}
    }
}

const conn = new axios.create({
    baseURL: "/api/"
})

conn.interceptors.request.use(async config => {
    await refreshJWTTokenIfNeeded(config)

    config.headers = {
        ...config.headers,
        ...authorizationToken()
    }

    return config
})

export default conn