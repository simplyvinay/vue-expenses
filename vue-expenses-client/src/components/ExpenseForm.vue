<template>
  <v-form class="mt-1" ref="expenseform">
    <v-container>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="expense.expenseCategory"
            :items="categories"
            item-text="name"
            item-value="id"
            label="Category"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :rules="[required('Category')]"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-menu v-model="dateMenu" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="expense.expenseDate"
                label="Date"
                readonly
                v-on="on"
                class="ma-0 pa-0 form-label"
                dense
                :rules="[required('Date')]"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="expense.expenseDate"
              @change="dateMenu = false"
              :no-title="true"
            ></v-date-picker>
          </v-menu>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            v-model="expense.expenseType"
            :items="types"
            item-text="name"
            item-value="id"
            label="Type"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
            :rules="[required('Type')]"
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-text-field
            v-model="expense.expenseAmount"
            label="Amount"
            required
            class="ma-0 pa-0 form-label"
            dense
            :rules="[required('Amount'), minValue('Amount', 0.01)]"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-textarea
        v-model="expense.expenseComments"
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
import validations from "@/helpers/validations";

export default {
  props: {
    expense: Object
  },
  data() {
    return {
      dateMenu: false,
      ...validations
    };
  },
  computed: {
    ...mapState("expenseCategories", ["categories"]),
    ...mapState("expenseTypes", ["types"])
  },
  methods: {
    handleSubmit() {
      if (!this.$refs.expenseform.validate()) return;
    }
  }
};
</script>

<style>
</style>