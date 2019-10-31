<template>
  <div>
    <h1 class="headline blue--text text--lighten-1">Overview</h1>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12 md5>
          <v-card class="pa-3 mr-2 mr-2" raised>
            <div class="body-1 pa-3 grey--text text--darken-2 text-capitalize">Add New Expense</div>
            <v-divider></v-divider>
            <v-form v-model="valid">
              <v-container>
                <v-textarea
                  v-model="description"
                  :rules="nameRules"
                  label="Description"
                  auto-grow="true"
                  required
                  clearable
                  class="ma-0 pa-0 form-label"
                  dense
                ></v-textarea>
                <v-row>
                  <v-col cols="12" md="6" class="py-0 ma-0">
                    <v-select
                      :items="items"
                      label="Category"
                      clearable
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
                          clearable
                          v-on="on"
                          class="ma-0 pa-0 form-label"
                          dense
                        ></v-text-field>
                      </template>
                      <v-date-picker v-model="date" @change="dateMenu = false"></v-date-picker>
                    </v-menu>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" md="6" class="py-0 ma-0">
                    <v-select
                      :items="items"
                      label="Type"
                      clearable
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
                      clearable
                      class="ma-0 pa-0 form-label"
                      dense
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row class="justify-end">
                  <v-btn outlined rounded class="blue--text">Submit</v-btn>
                </v-row>
              </v-container>
            </v-form>
          </v-card>
        </v-flex>
        <v-flex xs12 md5>
          <v-card class="pa-3 mr-2" outlined height="100%">
            <div class="body-1 pa-3 grey--text text--darken-2 text-capitalize">Monthly Budget</div>
            <v-divider></v-divider>
            <DoughnutChart :styles="graphsHeight1" />
            <v-container pb-0 pt-10>
              <v-layout row justify-space-around>
                <v-card flat max-width="200">
                  <div class="caption">Monthly Limit</div>
                  <span class="subtitle-2">35000</span>
                </v-card>

                <v-card flat max-width="200">
                  <div class="caption">Spent Amount</div>
                  <span class="subtitle-2">20000</span>
                </v-card>
              </v-layout>
            </v-container>
          </v-card>
        </v-flex>
        <v-flex
          xs12
          md2
        >Lorem ipsum dolor sit amet consectetur adipisicing elit. Dolore perferendis deleniti sapiente labore placeat aut mollitia. Aliquam officiis iusto qui pariatur, ducimus iste eveniet possimus ipsam error, suscipit deserunt dolor!</v-flex>
        <v-flex xs12 md12>
          <v-container>
            <v-layout row wrap>
              <v-flex xs12 md10>
                <v-card class="pa-3 mr-2" outlined>
                  <div class="body-1 pa-3 grey--text text--darken-2 text-capitalize">Breakdown</div>
                  <v-divider></v-divider>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12 md6>
                        <BarChart :styles="graphsHeight" />
                      </v-flex>
                      <v-flex xs12 md6>
                        <PieChart :styles="graphsHeight2" />
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import DoughnutChart from "@/components/DoughnutChart";
import BarChart from "@/components/BarChart";
import PieChart from "@/components/PieChart";

export default {
  components: { DoughnutChart, BarChart, PieChart },
  data() {
    return {
      dateMenu: false,
      graphsHeight: {
        height: "280px"
      },
      graphsHeight1: {
        height: "215px"
      },
      graphsHeight2: {
        height: "275px"
      }
    };
  }
};
</script>

<style>
.form-label label[for] {
  height: 20px;
  font-size: 10pt;
}
</style>