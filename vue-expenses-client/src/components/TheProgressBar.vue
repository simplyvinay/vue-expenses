<template>
  <v-progress-linear
    :active="loading"
    :height="3"
    :indeterminate="true"
    fixed
    color="blue lighten-2"
    :class="{'singleAppbar': $vuetify.breakpoint.smAndDown, 'doubleAppbar': $vuetify.breakpoint.mdAndUp}"
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
.singleAppbar {
  top: 50px;
}

.doubleAppbar {
  top: 98px;
}
</style>