import Vue from 'vue';
import Vuex from 'vuex';
import { alert } from '@/store/alert';
import { account } from '@/store/account';
import { type } from '@/store/type';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        alert,
        account,
        type
    }
});

export default store;