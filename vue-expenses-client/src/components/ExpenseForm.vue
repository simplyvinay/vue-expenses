<template>
  <v-form class="mt-1">
    <v-container>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="form.expenseCategory"
            :items="categories"
            item-text="name"
            item-value="id"
            label="Category"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :error-messages="expenseCategoryErrors"
            @blur="$v.form.expenseCategory.$touch()"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-menu v-model="dateMenu" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="form.expenseDate"
                label="Date"
                readonly
                v-on="on"
                class="ma-0 pa-0 form-label"
                dense
                :error-messages="expenseDateErrors"
                @blur="$v.form.expenseDate.$touch()"
              ></v-text-field>
            </template>
            <v-date-picker v-model="form.expenseDate" @change="dateMenu = false" :no-title="true"></v-date-picker>
          </v-menu>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="form.expenseType"
            :items="types"
            item-text="name"
            item-value="id"
            label="Type"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :error-messages="expensetypeErrors"
            @blur="$v.form.expenseType.$touch()"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-text-field
            v-model="form.expenseAmount"
            label="Amount"
            required
            class="ma-0 pa-0 form-label"
            dense
            :error-messages="expenseamountErrors"
            @blur="$v.form.expenseAmount.$touch()"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-textarea
        v-model="form.expenseComments"
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
        expenseCategory: "",
        expenseDate: "",
        expenseType: "",
        expenseAmount: "",
        expenseComments: ""
      }
    };
  },
  validations: {
    form: {
      expenseCategory: { required },
      expenseDate: { required },
      expenseType: { required },
      expenseAmount: { required, minValue: minValue(0.1) }
    }
  },
  computed: {
    ...mapState("expenseCategories", ["categories"]),
    ...mapState("expenseTypes", ["types"]),
    expenseCategoryErrors() {
      const errors = [];
      if (!this.$v.form.expenseCategory.$dirty) return errors;
      !this.$v.form.expenseCategory.required &&
        errors.push("Expense category is required");
      return errors;
    },
    expenseDateErrors() {
      const errors = [];
      if (!this.$v.form.expenseDate.$dirty) return errors;
      !this.$v.form.expenseDate.required &&
        errors.push("Expense date is required");
      return errors;
    },
    expensetypeErrors() {
      const errors = [];
      if (!this.$v.form.expenseType.$dirty) return errors;
      !this.$v.form.expenseType.required &&
        errors.push("Expense type is required");
      return errors;
    },
    expenseamountErrors() {
      const errors = [];
      if (!this.$v.form.expenseAmount.$dirty) return errors;
      !this.$v.form.expenseAmount.minValue &&
        errors.push("Must be greater than 0");
      !this.$v.form.expenseAmount.required &&
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