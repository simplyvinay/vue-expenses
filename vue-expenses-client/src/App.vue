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
}

::-webkit-scrollbar-thumb {
  background-color: #757575;
  background-image: -webkit-linear-gradient(
    45deg,
    rgba(255, 255, 255, 0.2) 25%,
    transparent 25%,
    transparent 50%,
    rgba(255, 255, 255, 0.2) 50%,
    rgba(255, 255, 255, 0.2) 75%,
    transparent 75%,
    transparent
  );
}
</style>