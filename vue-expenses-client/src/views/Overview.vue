<template>
  <div class="d-flex" style=" height: 100%">
    <div style="width: 75%;" class="grey lighten-4 pa-4">
      <v-layout row justify-start align-end pl-8>
        <!-- <v-icon medium color="deep-orange">shopping_basket</v-icon> -->
        <div class="ml-2 title-1 blue--text">Expenses</div>
      </v-layout>
      <v-container fluid>
        <v-row>
          <v-col class="elevation-0 pl-0">
            <v-row justify="center">
              <v-card flat tile class="rounded-card">
                <div class="d-flex">
                  <BarChart :styles="graphsHeight" />
                  <div style="width: 40%; border-left: 0.5px dashed lightgray">
                    <DoughnutChart :styles="graphsHeight1" />
                    <div class="d-flex mt-2">
                      <v-list-item-content class="pa-3 mt-3 ml-10">
                        <v-list-item-title class="subtitle-2">35,000</v-list-item-title>
                        <v-list-item-subtitle class="caption">Monthly Limit</v-list-item-subtitle>
                      </v-list-item-content>
                      <v-list-item-content class="pa-3 mt-3 ml-0">
                        <v-list-item-title class="subtitle-2">30,000</v-list-item-title>
                        <v-list-item-subtitle class="caption">Spent</v-list-item-subtitle>
                      </v-list-item-content>
                    </div>
                  </div>
                </div>
              </v-card>
            </v-row>
          </v-col>
        </v-row>
      </v-container>
      <div class="pl-3 gray--text" left>
        <span class="ml-2 title-1">Categories With Biggest Expenses</span>
      </div>
    </div>
    <div style="width: 25%;" class="white pl-3 pb-3">
      <v-layout row justify-start align-baseline pl-6 mt-4>
        <div class="ml-2 title-2 grey--text">Categories</div>
      </v-layout>
      <v-container fluid class="pl-3">
        <v-card v-for="link in links" :key="link.text" flat tile class="elevation-0">
          <v-row style="align-items: end">
            <v-col class="d-flex ml-4">
              <v-row>
                <v-icon medium left :color="link.color">{{link.icon}}</v-icon>
              </v-row>
              <v-row class="ml-4">
                <span class="body-2 font-weight-medium">{{link.text}}</span>
                <span style="font-size: 0.6rem; ">{{link.lastExpenseDate}}</span>
              </v-row>
            </v-col>
            <v-spacer></v-spacer>
            <v-col>
              <span class="body-2">{{link.value}}</span>
            </v-col>
          </v-row>
        </v-card>
      </v-container>
    </div>
  </div>
</template>

<script>
import BarChart from "@/components/BarChart";
import DoughnutChart from "@/components/DoughnutChart";

export default {
  components: { BarChart, DoughnutChart },
  data() {
    return {
      graphsHeight: {
        height: "280px"
      },
      graphsHeight1: {
        height: "200px"
      },
      links: [
        {
          icon: "insert_chart",
          text: "General",
          lastExpenseDate: "20/10/2019",
          value: "4,000",
          color: "orange"
        },
        {
          icon: "shopping_cart",
          text: "Shopping",
          lastExpenseDate: "20/10/2019",
          value: "200",
          color: "blue"
        },
        {
          icon: "hotel",
          text: "Travel",
          lastExpenseDate: "20/10/2019",
          value: "3,129",
          color: "green"
        },
        {
          icon: "lightbulb_outline",
          text: "Utilities",
          lastExpenseDate: "20/10/2019",
          value: "300",
          color: "purple"
        }
      ]
    };
  }
};
</script>

<style>
.main-span:before {
  content: "\A";
  white-space: pre;
}
</style>