const state = () => ({
    players: [
        { id: 1, name: "Patrick", color: "#e74c3c", owner: true, score: 345 },
        { id: 2, name: "Nikolaj", color: "#9b59b6", score: 765 },
        { id: 3, name: "Floris", color: "#000000", score: 585 },
        { id: 4, name: "Thor", color: "#ff0000", score: 895 },
        { id: 5, name: "Simon", color: "#00ff00", score: 570 }
    ],
    lobbyCode: "a2C4",
    chosenWord: "",
    wordList: [],
    roundOverview: null,

    settings: [
        { name: "Max players", value: "12" },
        { name: "Max rounds", value: "10" },
        { name: "Min rounds", value: "17" },
        { name: "Turn time", value: "10000 fisk" }
    ]
})

const getters = {
    isWordListEmpty: state => {
        return state.wordList.length === 0;
    },
    isRoundOverviewVisible: state => {
        return state.roundOverview != null;
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
    },
    roundOver({ commit }, status) {
        commit("setRoundOverview", status);
    }
}

const mutations = {
    setChosenWord(state, msg) {
        state.chosenWord = msg
    },
    setWords(state, words) {
        state.wordList = words;
    },
    setRoundOverview(state, status) {
        state.roundOverview = status
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