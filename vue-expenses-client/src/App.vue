<template>
  <v-app>
    <v-snackbar v-model="alert.show" :color="alert.color" top>
      {{alert.message}}
      <v-icon small dark @click="alert.show = false">close</v-icon>
    </v-snackbar>
    <router-view></router-view>
  </v-app>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "app",
  components: {},
  computed: {
    ...mapState({
      alert: state => state.alert.alert
    })
  },
  data() {
    return {};
  }
};
</script>