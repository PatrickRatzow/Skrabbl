import Vue from 'vue';
import Vuex from "vuex";
import App from './App.vue';
import store from "./store/index"
import router from './router'
import ws from "./signal-r"

Vue.config.productionTip = true;

Vue.prototype.$socket = ws

Vue.use(Vuex)

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
