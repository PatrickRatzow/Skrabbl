const state = () => ({
    players: [
        { id: 1, name: "Patrick", color: "#e74c3c", owner: true },
        { id: 2, name: "Nikolaj", color: "#9b59b6" }
    ],
    lobbyCode: "a2C4",
    chosenWord: "",
    wordList: [],
    roundOverview: null,
    pointList: [
        { player: "Patrick", score: 345 },
        { player: "Simon", score: 765 },
        { player: "Floris", score: 585 },
        { player: "Thor", score: 895 },
        { player: "Nikolaj", score: 570 },
    ]
})

const getters = {
    isWordListEmpty: state => {
        return state.wordList.length === 0;
    },
    isRoundOverviewVisible: state => {
        return state.roundOverview != null;
    },
    isNewScoresAdded: state => {
        return state.pointList != null;
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
    },
    newScoresAdded({ commit }, points) {
        commit("setNewScores", points);
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
    },
    setNewScores(state, points) {
        state.pointList = points
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