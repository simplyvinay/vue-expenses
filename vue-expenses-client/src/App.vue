<template>
  <v-app>
    <v-snackbar
      v-for="alert in alerts.filter(s => s.show)"
      :key="alert.message"
      v-model="alert.show"
      :color="alert.color"
      top
    >
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
      alerts: state => state.alert.alerts
    })
  },
  data() {
    return {
      
    };
  }
};
</script>