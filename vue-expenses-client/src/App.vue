<template>
  <v-app>
    <v-snackbar v-model="alert.show" :color="alert.color" top>
      {{alert.message}}
      <template v-slot:action="{ attrs }">
        <v-icon small dark v-bind="attrs" @click="alert.show = false">close</v-icon>
      </template>
    </v-snackbar>
    <router-view class="my-3"></router-view>
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

<style lang="scss">
.container {
  max-width: 1185px !important;
}
</style>