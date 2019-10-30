<template>
  <v-app style="max-width: 1024px; margin: auto;">
    <Navbar />
    <v-content style="height: 100%">
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
import Navbar from "./components/Navbar";
export default {
  name: "App",
  components: {
    Navbar
  },
  data: () => ({
    //
  })
};
</script>

<style>
.rounded-card {
  border-radius: 10px;
}
</style>