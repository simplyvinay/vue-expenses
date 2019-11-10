<template>
  <v-app :style="{background: $vuetify.theme.themes[isDark].background}">
    <v-snackbar
      v-for="alert in alerts.filter(s => s.show)"
      :key="alert.show"
      v-model="alert.show"
      :color="alert.color"
      top="true"
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
    isDark() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    },
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