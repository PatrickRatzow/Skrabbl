import DrawService from "../../services/draw.service";

const state = () => ({
    sizes: [1, 2, 4, 8, 16, 32, 64],
    colors: [
        { name: "Black", backgroundColor: "#000000" },
        { name: "Red", backgroundColor: "#ff0000" },
        { name: "Blue", backgroundColor: "#0000ff" },
        { name: "Green", backgroundColor: "#00ff00" },
        { name: "Light Grey", backgroundColor: "#d5d5d5", textColor: "black" }
    ],
    stroke: {
        color: { name: "Black", backgroundColor: "#000000" },
        size: 1
    }
})

const getters = {
    color: state => {
        return state.stroke.color
    },
    size: state => {
        return state.stroke.size
    }
}

const actions = {
    setColor({ commit }, color) {
        commit("setColor", color)
        return DrawService.sendColor(color);
    },
    setSize({ commit }, size) {
        commit("setSize", size)
    }
}

const mutations = {
    setColor(state, color) {
        state.stroke.color = color
    },
    setSize(state, size) {
        state.stroke.size = size
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
