<template>
  <div>
    <h1 class="headline blue--text text--lighten-1">Settings</h1>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12 md8>
          <v-card class="pa-3" flat>
            <v-form v-model="valid" class="xs12">
              <v-container>
                <v-text-field
                  v-model="currency"
                  :rules="currencyRules"
                  label="Currency"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-text-field
                  v-model="monthlyBudget"
                  :rules="budgetRules"
                  label="Monthly Budget"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-text-field>
                <v-switch v-model="mandatory" class="ma-0 pa-0 form-label" color="grey" label="Dark Theme"></v-switch>
                <v-row class="justify-end">
                  <v-btn outlined rounded class="blue--text">Submit</v-btn>
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
export default {};
</script>

<style>
</style>