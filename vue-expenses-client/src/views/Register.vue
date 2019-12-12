<template>
  <v-app>
    <v-content>
      <v-container fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card tile>
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
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click.native="handleLoginClick"
                >SignIn</v-btn>
                <v-spacer></v-spacer>
                <v-btn
                  small
                  outlined
                  class="primary--text"
                  @click="handleRegisterSubmit"
                  :loading="loading"
                >Register</v-btn>
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
import { REGISTER } from "@/store/_actiontypes";
import validations from "@/helpers/validations";
import router from "@/router/index";

export default {
  data() {
    return {
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
  methods: {
    ...mapActions("account", [REGISTER]),
    handleRegisterSubmit() {
      if (!this.$refs.registerForm.validate()) return;

      const { email, firstName, lastName, password } = this.registerForm;
      this.REGISTER({ email, firstName, lastName, password });
    },
    handleLoginClick() {
     router.push("/login");
    }
  }
};
</script>

<style>
</style>