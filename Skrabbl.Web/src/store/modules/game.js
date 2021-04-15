import ws from "../../signal-r"

const state = () => ({
    players: [
        { id: 1, name: "Patrick", color: "#e74c3c", owner: true },
        { id: 2, name: "Nikolaj", color: "#9b59b6" }
    ],
    lobbyCode: "a2C4",
    chosenWord: "",
    wordList: []
})

const getters = {
    isWordListEmpty: state => {
        return state.wordList.length === 0;
    }
}

//To trigger in browser
//document.getElementById("app").__vue__.$store.dispatch("game/setWords", ["Cake", "Hest", "XD"])
const actions = {
    chooseWord({ commit }, msg) {
        commit("setChosenWord", msg);
        commit("setWords", []);
        //ws.invoke("setChosenWord", msg)
    }, 
    setWords({ commit }, words) {
        commit("setWords", words);
    }
}

const mutations = {
    setChosenWord(state, msg) {
        state.chosenWord = msg
    },
    setWords(state, words) {
        state.wordList = words;
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