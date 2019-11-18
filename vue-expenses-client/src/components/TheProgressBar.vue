<template>
  <v-progress-linear
    :active="loading"
    :height="3"
    :indeterminate="true"
    :top="true"
    color="pink lighten-1"
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