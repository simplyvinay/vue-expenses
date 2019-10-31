<template>
  <v-app>
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
  data: () => ({
    //
  })
};
</script>
