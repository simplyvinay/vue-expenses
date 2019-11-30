<template>
  <div>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12>
          <v-layout row justify-space-between>
            <v-flex xs12 md4>
              <v-card class="pa-2 mr-2" flat min-height="340px" height="100%">
                <div
                  class="blue--text px-2 py-1 text-capitalize font-weight-medium"
                >General Settings</div>
                <v-divider></v-divider>
                <v-form class="xs12">
                  <v-container>
                    <v-text-field
                      label="System Name"
                      required
                      clearable
                      class="ma-0 pa-0 form-label"
                      dense
                    ></v-text-field>
                    <v-text-field
                      label="Display Currency"
                      required
                      clearable
                      class="ma-0 pa-0 form-label"
                      dense
                      @input="updateLocalSettings('currency', $event)"
                    ></v-text-field>
                    <v-switch
                      class="ma-0 pa-0 form-label"
                      color="grey"
                      label="Dark Theme"
                      dense
                      v-model="useDarkMode"
                      @change="updateLocalSettings('useDarkMode', $event)"
                    ></v-switch>

                    <v-row class="justify-end">
                      <v-btn
                        outlined
                        small
                        class="blue--text font-weight-bold"
                        @click="handleSubmit"
                        :loading="loading"
                      >Submit</v-btn>
                    </v-row>
                  </v-container>
                </v-form>
              </v-card>
            </v-flex>
            <v-flex xs12 md8>
              <v-card
                :class="{'pa-2 mr-2 mt-2': $vuetify.breakpoint.smAndDown, 'pa-2 mr-2': $vuetify.breakpoint.mdAndUp}"
                flat
                min-height="340px"
                height="100%"
              >
                <ExpenseTypes />
              </v-card>
            </v-flex>
          </v-layout>
        </v-flex>

        <v-flex xs12>
          <v-container px-0 pb-0>
            <v-layout row justify-space-between>
              <v-flex xs12>
                <v-card class="pa-2 mr-2" flat min-height="340px" height="100%">
                  <ExpenseCategories />
                </v-card>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { EDIT_USER_SETTINGS } from "@/store/_actiontypes";
import ExpenseCategories from "@/components/ExpenseCategories";
import ExpenseTypes from "@/components/ExpenseTypes";

export default {
  components: {
    ExpenseCategories,
    ExpenseTypes
  },
  computed: {
    ...mapState({
      useDarkMode: state => state.account.user ? state.account.user.useDarkMode : false
    }),
  },
  methods: {
    ...mapActions("account", [EDIT_USER_SETTINGS]),
    updateLocalSettings(id, value) {
      this.$set(this.settings, id, value);
    },
    handleSubmit() {
      this.loading = true;
      this.EDIT_USER_SETTINGS(this.settings)
      .finally(() => {
        this.loading = false;
      });
    }
  },
  data: () => ({
    loading: false,
    settings: {}
  })
};
</script>

<style>
</style>