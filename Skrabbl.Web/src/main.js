import Vue from 'vue';
import Vuex from "vuex";
import App from './App.vue';
import store from "./store/index"
import router from './router'
import ws from "./utils/signal-r"

Vue.config.productionTip = true;

Vue.prototype.$socket = ws

Vue.use(Vuex)

const app = new Vue({
    data: {
        loading: false
    },
    router,
    store,
    render: h => h(App)
}).$mount('#app');

router.beforeEach((to, from, next) => {
    app.loading = true

    next()
})
router.afterEach(() => {
    app.loading = false
})