import store from "../store/index"
import axios from "axios";

function authorizationToken() {
    if (store.state.user.jwt) {
        return { Authorization: `Bearer ${store.state.user.jwt}` }
    } else {
        return {}
    }
}

const conn = new axios.create({
    baseURL: "/api"
})

conn.interceptors.request.use(config => {
    config.headers = {
        ...config.headers,
        ...authorizationToken()
    }

    return config
})

export default conn