<template>
  <div>
    <v-container>
      <v-layout row justify-space-around>
        <v-flex xs12 sm8>
          <v-card class="pa-2 mr-2" flat height="100%">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Profile</div>
            <v-divider></v-divider>
            <v-form class="xs12 my-1">
              <v-container>
                <v-text-field
                  label="Email"
                  class="ma-0 pa-0 form-label"
                  v-model="profile.email"
                  dense
                  readonly
                ></v-text-field>
                <v-text-field
                  label="First Name"
                  required
                  dense
                  class="ma-0 pa-0 form-label"
                  v-model="profile.firstName"
                  :rules="[required('First Name')]"
                ></v-text-field>
                <v-text-field
                  label="Last Name"
                  required
                  class="ma-0 pa-0 form-label"
                  v-model="profile.lastName"
                  dense
                ></v-text-field>
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
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";
import validations from "@/helpers/validations";
import { EDIT_USER_PROFILE } from "@/store/_actiontypes";

export default {
  computed: {
    ...mapState({
      user: state => state.account.user
    })
  },
  methods: {
    ...mapActions("account", [EDIT_USER_PROFILE]),
    handleSubmit() {
      this.loading = true;
      this.EDIT_USER_PROFILE(this.profile).finally(() => {
        this.loading = false;
      });
    }
  },
  mounted() {
    this.profile = {
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      email: this.user.email
    };
  },
  data: () => ({
    loading: false,
    profile: {},
    ...validations
  })
};
</script>

<style>
</style> 