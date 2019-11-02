<template>
  <v-app :style="{background: $vuetify.theme.themes[isDark].background}">
    <v-container pt-0>
      <Navbar />
      <v-content class="pa-4">
        <router-view></router-view>
      </v-content>
    </v-container>
  </v-app>
</template>

<script>
import Navbar from "./components/Navbar";
export default {
  name: "App",
  components: {
    Navbar
  },
  computed: {
    isDark() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    }
  },
  created(){
    //this.$vuetify.theme.dark = true
  },
  data: () => ({
    //
  })
};
</script>
