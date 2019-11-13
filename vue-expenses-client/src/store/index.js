import Vue from 'vue';
import Vuex from 'vuex';
import { alert } from '@/store/alert';
import { loader } from '@/store/loader';
import { account } from '@/store/account';
import { expensetype } from '@/store/expensetype';


Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        alert,
        loader,
        account,
        expensetype
    }
});

export default store;