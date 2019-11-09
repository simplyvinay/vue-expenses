<template>
  <v-app :style="{background: $vuetify.theme.themes[isDark].background}">
    <Navbar />
    <v-container pt-2>
      <v-content class="pt-1 pl-1">
        <router-view></router-view>
      </v-content>
    </v-container>
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar";
export default {
  components: {
    Navbar
  },
  computed: {
    isDark() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    }
  },
  created() {
    //this.$vuetify.theme.dark = true
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