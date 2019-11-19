<template>
  <v-app>
    <v-content :dark="true">
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card tile>
              <v-toolbar flat color="primary" dark>
                <v-toolbar-title>Login</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="loginform">
                  <v-text-field
                    v-model="form.email"
                    label="E-mail"
                    prepend-icon="email"
                    :rules="[required('Email'), email('Email')]"
                  ></v-text-field>
                  <v-text-field
                    v-model="form.password"
                    label="Password"
                    prepend-icon="lock"
                    :type="showPassword ? 'text' : 'password'" 
                    :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                    @click:append="showPassword = !showPassword"
                    :rules="[required('Password')]"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn outlined class="primary--text" @click="handleSubmit" :loading="loading">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { LOGIN, LOGOUT } from "@/store/_actiontypes";
import validations from "@/helpers/validations";

export default {
  data() {
    return {
      form: {
        email: "test@demo.com",
        password: ""
      },
      showPassword: false,
      ...validations
    };
  },
  computed: {
    ...mapState({
      loading: state => state.loader.loading
    })
  },
  created() {
    //reset theme
    this.$vuetify.theme.dark = 0;
    // reset login status
    this.LOGOUT();
  },
  methods: {
    ...mapActions("account", [LOGIN, LOGOUT]),
    handleSubmit() {
      if (!this.$refs.loginform.validate()) return;

      const { email, password } = this.form;
      if (email && password) {
        this.LOGIN({ email, password });
      }
    }
  }
};
</script>

<style>
</style>