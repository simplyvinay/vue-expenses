<template>
  <v-container pb-0>
    <v-layout row wrap>
      <v-flex xs12 md12>
        <v-card class="pa-2 mr-2" tile>
          <div class="d-flex align-center">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Expenses Breakdown</div>
            <div class="ml-2">
              <v-select
                :items="years"
                dense
                label="Year"
                width="50"
                v-model="selectedYear"
                @change="loaddata"
              ></v-select>
            </div>
          </div>
          <v-divider></v-divider>
          <v-container>
            <v-layout row wrap>
              <v-flex xs12 md6>
                <v-data-table
                  :headers="headers"
                  :items="yearlyExpenses"
                  sort-by="date"
                  :items-per-page="5"
                  loading-text="Loading... Please wait"
                  :footer-props="{itemsPerPageOptions: [5]}"
                >
                  <template #item.value="{ value }">
                    <span>{{value.toFixed(2)}}</span>
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
import StackChart from "@/components/StackChart";

export default {
  props: {
    years: {
      type: Array,
      default() {
        return [];
      }
    }
  },
  components: {
    StackChart
  },
  mounted() {
    this.loadyearlydata(this.selectedYear);
    this.loadcategoryStack(this.selectedYear);
    this.theme = this.$vuetify.theme.dark ? "dark" : "";
  },
  computed: {},
  methods: {
    loaddata(year) {
      this.loadyearlydata(year);
      this.loadcategoryStack(year);
    },
    loadyearlydata(year) {
      Api.get(`/expenses/getbyyear/${this.selectedYear}`).then(response => {
        this.yearlyExpenses = response.data;
      });
    },
    loadcategoryStack(year) {
      Api.get(`/statistics/getexpenesesbycategory/${this.selectedYear}`).then(
        response => {
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
            legendData: ['Utilities', 'Travel'],
            data: response.data
          };
        }
      );
    }
  },
  data: () => ({
    theme: "",
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