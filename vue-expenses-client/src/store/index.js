import Vue from 'vue';
import Vuex from 'vuex';
import { alert } from '@/store/modules/alert';
import { loader } from '@/store/modules/loader';
import { account } from '@/store/modules/account';
import { expenseTypes } from '@/store/modules/expensetypes';
import { expenseCategories } from '@/store/modules/expensecategories';
import { expenses } from '@/store/modules/expenses';
import { statistics } from '@/store/modules/statistics';
import createPersistedState from "vuex-persistedstate";
import SecureLS from "secure-ls";
const ls = new SecureLS({ isCompression: false });

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        alert,
        loader,
        account,
        expenseTypes,
        expenseCategories,
        expenses,
        statistics
    },
    plugins: [
        createPersistedState({
          storage: {
            getItem: key => ls.get(key),
            setItem: (key, value) => ls.set(key, value),
            removeItem: key => ls.remove(key)
          }
        })
      ],
});

export default store;