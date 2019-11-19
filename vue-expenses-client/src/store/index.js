import Vue from 'vue';
import Vuex from 'vuex';
import { alert } from '@/store/modules/alert';
import { loader } from '@/store/modules/loader';
import { account } from '@/store/modules/account';
import { expensetypes } from '@/store/modules/expensetypes';
import { expensecategories } from '@/store/modules/expensecategories';
import { statistics } from '@/store/modules/statistics';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        alert,
        loader,
        account,
        expensetypes,
        expensecategories,
        statistics
    }
});

export default store;