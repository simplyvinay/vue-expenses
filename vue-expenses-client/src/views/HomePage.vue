<template>
  <div>
    <Navbar />
    <v-container pt-2>
      <v-content class="pt-1 pl-1">
        <router-view></router-view>
      </v-content>
    </v-container>    
  </div>
</template>

<script>
import Navbar from "@/components/TheNavbar";
import { LOAD_EXPENSE_TYPES, LOAD_CATEGORIES } from "@/store/_actionTypes";
export default {
  components: {
    Navbar
  },
  mounted: function() {
    this.$store.dispatch(`expensetypes/${LOAD_EXPENSE_TYPES}`);
    this.$store.dispatch(`expensecategories/${LOAD_CATEGORIES}`);
  },
  created: function(){
    this.$vuetify.theme.dark = JSON.parse(localStorage.getItem('user')).useDarkMode;
  },
  data: () => ({
    //
  })
};
</script>
<style>
/* scroll bar style */
::-webkit-scrollbar-track {
  --webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
  background-color: #f5f5f5;
}

::-webkit-scrollbar {
  width: 10px;
  height: 8px;
}

::-webkit-scrollbar-thumb {
  background-color: #757575;
}

.form-label label[for] {
  height: 20px;
  font-size: 10pt;
}

.tableHeader {
  height: 20px;
  font-size: 16px !important;
}
</style>