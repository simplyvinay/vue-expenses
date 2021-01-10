<template>
  <div>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12>
          <v-layout row justify-space-between>
            <v-flex xs12 md4 my-3>
              <v-card class="pa-2 mr-2" flat min-height="340px" height="100%">
                <div
                  class="blue--text px-2 py-1 text-capitalize font-weight-medium"
                >General Settings</div>
                <v-divider></v-divider>
                <v-form class="xs12 my-1">
                  <v-container>
                    <v-text-field
                      label="System Name"
                      required
                      class="ma-0 pa-0 form-label"
                      v-model="settings.systemName"
                      dense
                    ></v-text-field>
                    <v-autocomplete
                      v-model="settings.currencyRegionName"
                      :items="currencies"
                      item-text="name"
                      item-value="id"
                      label="Display Currency"
                      class="ma-0 pa-0 form-label"
                      dense
                    ></v-autocomplete>
                    <v-switch
                      class="ma-0 pa-0 form-label"
                      color="grey"
                      label="Dark Theme"
                      dense
                      v-model="settings.useDarkMode"
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
            <v-flex xs12 md8 my-3>
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
              <v-flex xs12 >
                <v-card class="pa-2 mr-2 my-3" flat min-height="340px" height="100%">
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
      user: state => state.account.user,
      currencies: state => state.account.currencies
    })
  },
  methods: {
    ...mapActions("account", [EDIT_USER_SETTINGS]),
    handleSubmit() {
      this.loading = true;
      this.EDIT_USER_SETTINGS(this.settings).finally(() => {
        this.loading = false;
      });
    }
  },
  mounted() {
    this.settings = {
      systemName: this.user.systemName,
      useDarkMode: this.user.useDarkMode,
      currencyRegionName: this.user.currencyRegionName
    };
  },
  data: () => ({
    loading: false,
    settings: {},
  })
};
</script>

<style>
</style>