<template>
  <v-app>
    <v-content>
      <v-container fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card tile v-if="showRegistrationForm">
              <v-toolbar flat color="primary" dark>
                <v-toolbar-title>Register</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="registerForm">
                  <v-text-field
                    v-model="registerForm.email"
                    placeholder="E-mail"
                    :rules="[required('Email'), email('Email')]"
                    dense
                  ></v-text-field>
                  <v-text-field
                    v-model="registerForm.firstName"
                    placeholder="First Name"
                    :rules="[required('FirstName')]"
                    dense
                  ></v-text-field>
                  <v-text-field v-model="registerForm.lastName" placeholder="Last Name" dense></v-text-field>
                  <v-text-field
                    v-model="registerForm.password"
                    placeholder="Password"
                    :type="showRegisterPassword ? 'text' : 'password'"
                    :append-icon="showRegisterPassword ? 'mdi-eye' : 'mdi-eye-off'"
                    :rules="[required('Password')]"
                    dense
                  >
                    <v-icon
                      slot="append"
                      small
                      v-if="showRegisterPassword"
                      @click="showRegisterPassword = !showRegisterPassword"
                    >mdi-eye</v-icon>
                    <v-icon
                      slot="append"
                      small
                      v-else
                      @click="showRegisterPassword = !showRegisterPassword"
                    >mdi-eye-off</v-icon>
                  </v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click="showRegistrationForm = !showRegistrationForm"
                >SignIn</v-btn>
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click="handleRegisterSubmit"
                  :loading="loading"
                >Register</v-btn>
              </v-card-actions>
            </v-card>
            <v-card tile v-else>
              <v-toolbar flat color="primary" dark>
                <v-toolbar-title>Login</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="loginForm">
                  <v-text-field
                    v-model="loginForm.email"
                    placeholder="E-mail"
                    prepend-icon="email"
                    :rules="[required('Email'), email('Email')]"
                    dense
                  ></v-text-field>
                  <v-text-field
                    v-model="loginForm.password"
                    placeholder="Password"
                    prepend-icon="lock"
                    :type="showPassword ? 'text' : 'password'"
                    :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                    @click:append="showPassword = !showPassword"
                    :rules="[required('Password')]"
                    dense
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click="showRegistrationForm = !showRegistrationForm"
                >Register</v-btn>
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click="handleLoginSubmit"
                  :loading="loading"
                >Login</v-btn>
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
import { LOGIN, LOGOUT, REGISTER } from "@/store/_actiontypes";
import validations from "@/helpers/validations";

export default {
  data() {
    return {
      showRegistrationForm: false,
      loginForm: {
        email: "test@demo.com",
        password: ""
      },
      registerForm: {
        email: "",
        firstName: "",
        lastName: "",
        password: ""
      },
      showRegisterPassword: false,
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
    ...mapActions("account", [LOGIN, LOGOUT, REGISTER]),
    handleLoginSubmit() {
      if (!this.$refs.loginForm.validate()) return;

      const { email, password } = this.loginForm;
      if (email && password) {
        this.LOGIN({ email, password });
      }
    },
    handleRegisterSubmit() {
      if (!this.$refs.registerForm.validate()) return;

      const { email, firstName, lastName, password } = this.registerForm;
      this.REGISTER({ email, firstName, lastName, password });
    }
  }
};
</script>

<style>
</style>