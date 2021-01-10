<template>
  <v-container pb-0>
    <v-layout row wrap>
      <v-flex xs12 md12>
        <v-card class="pa-2 mr-2" tile>
          <div class="d-flex align-baseline">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Expenses Breakdown</div>
            <div class="ml-2 mt-2">
              <v-autocomplete
                :items="years"
                dense
                label="Year"
                v-model="selectedYear"
                @change="loaddata"
              ></v-autocomplete>
            </div>
          </div>
          <v-divider></v-divider>
          <v-container>
            <v-layout row wrap my-0>
              <v-flex xs12 md6 mt-4>
                <v-data-table
                  height="280px"
                  :headers="headers"
                  :items="yearlyExpenses"
                  sort-by="date"
                  :items-per-page="5"
                  loading-text="Loading... Please wait"
                  dense
                  :footer-props="{itemsPerPageOptions: [10]}"
                >
                  <template #item.value="{ value }">
                    <span>{{`${user.displayCurrency} ${value.toFixed(2)}`}}</span>
                  </template>
                </v-data-table>
              </v-flex>
              <v-flex xs12 md6 style="min-height:340px; height=100%">
                <StackChart :theme="theme" :height="100" :seriesData="categoryStack" />
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Api from "@/services/api";
import StackChart from "@/components/Charts/StackChart";
import { mapState, mapActions } from "vuex";
import forEach from "lodash/forEach";
import find from "lodash/find";
import groupBy from "lodash/groupBy";
import map from "lodash/map";

export default {
  props: {
    years: {
      type: Array,
      default() {
        return [];
      }
    },
    theme: {
      type: String
    }
  },
  components: {
    StackChart
  },
  mounted() {
    this.loaddata(this.selectedYear);
  },
  computed: {
    ...mapState({
      categories: state => state.expenseCategories.categories,
      user: state => state.account.user
    })
  },
  methods: {
    loaddata(year) {
      this.loadyearlydata(year).then(() => {
        this.loadcategoryStack(year);
      });
    },
    loadyearlydata(year) {
      return Api.get(`/expenses/getbyyear/${this.selectedYear}`).then(
        response => {
          this.yearlyExpenses = response.data;
        }
      );
    },
    loadcategoryStack(year) {
      let expenses = this.yearlyExpenses;
      let categories = this.categories;

      let expensesByMonth = [];
      forEach(categories, (value, key) => {
        Array.from(Array(12).keys(), month => {
          let x = expenses.find(
            ex => ex && +ex.month === month + 1 && ex.category == value.name
          );
          expensesByMonth.push(
            x || {
              category: value.name,
              categoryBudget: value.budget,
              categoryColour: value.colourHex,
              categoryId: value.id,
              comments: null,
              date: null,
              id: null,
              month: month + 1,
              type: null,
              typeId: null,
              value: 0
            }
          );
        });
      });

      this.categoryStack = {
        xAxisData: [
          "Jan",
          "Feb",
          "Mar",
          "Apr",
          "May",
          "Jun",
          "Jul",
          "Aug",
          "Sep",
          "Oct",
          "Nov",
          "Dec"
        ],
        legendData: this.categories.map(x => x.name),
        data: map(
          groupBy(expensesByMonth, e => {
            return e.category + "|" + e.categoryColour;
          }),
          (cat, key) => {
            return {
              name: key.split("|")[0],
              color: key.split("|")[1],
              data: cat.map(x => x.value.toFixed(2))
            };
          }
        )
      };
    }
  },
  data: () => ({
    selectedYear: new Date().getFullYear(),
    yearlyExpenses: [],
    categoryStack: {},
    headers: [
      { text: "Category", value: "category" },
      { text: "Type", value: "type" },
      { text: "Date", value: "date" },
      { text: "Value", value: "value" }
    ]
  })
};
</script>

<style>
</style>