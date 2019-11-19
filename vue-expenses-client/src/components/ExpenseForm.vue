<template>
  <v-form class="mt-1">
    <v-container>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="form.expensecategory"
            :items="expensecategories"
            item-text="name"
            item-value="id"
            label="Category"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :error-messages="expensecategoryErrors"
            @blur="$v.form.expensecategory.$touch()"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-menu v-model="dateMenu" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="form.expensedate"
                label="Date"
                readonly
                v-on="on"
                class="ma-0 pa-0 form-label"
                dense
                :error-messages="expensedateErrors"
                @blur="$v.form.expensedate.$touch()"
              ></v-text-field>
            </template>
            <v-date-picker v-model="form.expensedate" @change="dateMenu = false" :no-title="true"></v-date-picker>
          </v-menu>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="form.expensetype"
            :items="expensetypes"
            item-text="name"
            item-value="id"
            label="Type"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :error-messages="expensetypeErrors"
            @blur="$v.form.expensetype.$touch()"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-text-field
            v-model="form.expenseamount"
            label="Amount"
            required
            class="ma-0 pa-0 form-label"
            dense
            :error-messages="expenseamountErrors"
            @blur="$v.form.expenseamount.$touch()"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-textarea
        v-model="form.expensedescription"
        label="Description"
        :auto-grow="true"
        required
        clearable
        class="ma-0 pa-0 form-label"
        dense
      ></v-textarea>
      <v-row class="justify-end">
        <v-btn outlined small class="blue--text font-weight-bold" @click="handleSubmit">Submit</v-btn>
      </v-row>
    </v-container>
  </v-form>
</template>

<script>
import { mapState } from "vuex";
import { required, minValue } from "vuelidate/lib/validators";

export default {
  data() {
    return {
      dateMenu: false,
      form: {
        expensecategory: "",
        expensedate: "",
        expensetype: "",
        expenseamount: "",
        expensedescription: ""
      }
    };
  },
  validations: {
    form: {
      expensecategory: { required },
      expensedate: { required },
      expensetype: { required },
      expenseamount: { required, minValue: minValue(0.1) }
    }
  },
  computed: {
    ...mapState("expensecategories", ["expensecategories"]),
    ...mapState("expensetypes", ["expensetypes"]),
    expensecategoryErrors() {
      const errors = [];
      if (!this.$v.form.expensecategory.$dirty) return errors;
      !this.$v.form.expensecategory.required &&
        errors.push("Expense category is required");
      return errors;
    },
    expensedateErrors() {
      const errors = [];
      if (!this.$v.form.expensedate.$dirty) return errors;
      !this.$v.form.expensedate.required &&
        errors.push("Expense date is required");
      return errors;
    },
    expensetypeErrors() {
      const errors = [];
      if (!this.$v.form.expensetype.$dirty) return errors;
      !this.$v.form.expensetype.required &&
        errors.push("Expense type is required");
      return errors;
    },
    expenseamountErrors() {
      const errors = [];
      if (!this.$v.form.expenseamount.$dirty) return errors;
      !this.$v.form.expenseamount.minValue &&
        errors.push("Must be greater than 0");
      !this.$v.form.expenseamount.required &&
        errors.push("Expense amount is required");
      return errors;
    }
  },
  methods: {
    handleSubmit() {
      this.$v.form.$touch();
      if (this.$v.form.$error) return;
    }
  }
};
</script>

<style>
</style>