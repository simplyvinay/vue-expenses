<template>
  <v-form v-model="valid" class="mt-1">
    <v-container>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            :items="expensecategories"
            item-text="name"
            item-value="id"
            label="Category"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-menu v-model="dateMenu" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="date"
                label="Date"
                readonly
                v-on="on"
                class="ma-0 pa-0 form-label"
                dense
              ></v-text-field>
            </template>
            <v-date-picker v-model="date" @change="dateMenu = false" no-title="true"></v-date-picker>
          </v-menu>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-select
            :items="expensetypes"
            item-text="name"
            item-value="id"
            label="Type"
            class="ma-0 pa-0 form-label"
            dense
            offset-x
          ></v-select>
        </v-col>
        <v-col cols="12" md="6" class="py-0 ma-0">
          <v-text-field
            v-model="amount"
            :rules="nameRules"
            label="Amount"
            required
            class="ma-0 pa-0 form-label"
            dense
          ></v-text-field>
        </v-col>
      </v-row>
      <v-textarea
        v-model="description"
        :rules="nameRules"
        label="Description"
        :auto-grow="true"
        required
        clearable
        class="ma-0 pa-0 form-label"
        dense
      ></v-textarea>
      <v-row class="justify-end">
        <v-btn outlined small class="blue--text font-weight-bold">Submit</v-btn>
      </v-row>
    </v-container>
  </v-form>
</template>

<script>
import { mapState } from "vuex";
export default {
  computed: {
    ...mapState("expensecategories", ["expensecategories"]),
    ...mapState("expensetypes", ["expensetypes"])
  }
};
</script>

<style>
</style>