import Vue from 'vue';
import Vuex from 'vuex';
import { account } from '@/store/account';
import { alert } from '@/store/alert';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        account,
        alert
    }
});

export default store;