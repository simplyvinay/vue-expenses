<template>
  <v-progress-linear
    :active="loading"
    :height="3"
    :indeterminate="true"
    fixed
    color="blue lighten-2"
    :class="{'singlebar': $vuetify.breakpoint.smAndDown, 'doublebar': $vuetify.breakpoint.mdAndUp}"
  ></v-progress-linear>
</template>
<script>
import { mapState } from "vuex";
export default {
  computed: {
    ...mapState({
      loading: state => state.loader.loading
    })
  }
};
</script>
<style scoped>
.singlebar {
  top: 50px;
}

.doublebar {
  top: 98px;
}
</style>