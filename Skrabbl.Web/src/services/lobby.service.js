import axios from "../utils/axios"

class LobbyService {
    joinLobby(id) {
        return axios.post(`/gamelobby/join/${id}`)
    }
}

export default new LobbyService()
