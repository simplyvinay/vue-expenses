<template>
  <v-app>
    <v-content :dark="true">
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card tile>
              <v-toolbar flat color="primary" dark >
                <v-toolbar-title>Login</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field
                    v-model="form.email"
                    label="E-mail"
                    required
                    :error-messages="emailErrors"
                    @input="$v.form.email.$touch()"
                    @blur="$v.form.email.$touch()"
                    prepend-icon="email"
                  ></v-text-field>
                  <v-text-field
                    v-model="form.password"
                    label="Password"
                    :error-messages="passwordErrors"
                    @input="$v.form.password.$touch()"
                    @blur="$v.form.password.$touch()"
                    prepend-icon="lock"
                    type="password"
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
import { required, email } from "vuelidate/lib/validators";
import { LOGIN, LOGOUT } from '@/store/_actionTypes'

export default {
  data() {
    return {
      form: {
        email: "test@demo.com",
        password: "",
        submitted: false
      }
    };
  },
  validations: {
    form: {
      email: { required, email },
      password: { required }
    }
  },
  computed: {
      ...mapState("loader", ["loading"]),
    emailErrors() {
      const errors = [];
      if (!this.$v.form.email.$dirty) return errors;
      !this.$v.form.email.email && errors.push("Must be valid e-mail");
      !this.$v.form.email.required && errors.push("E-mail is required");
      return errors;
    },
    passwordErrors() {
      const errors = [];
      if (!this.$v.form.password.$dirty) return errors;
      !this.$v.form.password.required && errors.push("Password is required");
      return errors;
    }
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
      this.$v.form.$touch();
      if (this.$v.form.$error) return;

      this.form.submitted = true;
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