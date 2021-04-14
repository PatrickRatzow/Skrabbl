import ws from "../../signal-r"

const state = () => ({
    players: [
        { id: 1, name: "Patrick", color: "#e74c3c", owner: true },
        { id: 2, name: "Nikolaj", color: "#9b59b6" }
    ],
    lobbyCode: "a2C4",
    chosenWord: "",
    wordList: ["Cake","Hest","Spil"]
})

const getters = {
    isWordListEmpty: state => {
        return state.wordList.length === 0;
    }
}

const actions = {
    chooseWord({ commit }, msg) {
        commit("setChosenWord", msg);
        commit("clearWordList");
        //ws.invoke("setChosenWord", msg)
    }
}

const mutations = {
    setChosenWord(state, msg) {
        state.chosenWord = msg
    },
    clearWordList(state) {
        state.wordList = []
    }
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