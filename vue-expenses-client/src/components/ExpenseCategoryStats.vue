<template>
  <v-container pb-0>
    <v-layout row wrap>
      <v-flex xs12 md12>
        <v-card class="pa-2 mr-2" tile>
          <div class="d-flex align-baseline">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Category Breakdown</div>
            <div class="ml-2 mt-2">
              <v-autocomplete
                :items="years"
                dense
                label="Year"
                v-model="selectedYear"
                @change="loaddata"
              ></v-autocomplete>
            </div>
            <div class="ml-2 mt-2">
              <v-autocomplete
                :items="months"
                dense
                label="Month"
                item-text="name"
                item-value="value"
                v-model="selectedMonth"
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
                  :items="categoryBreakdown"
                  sort-by="name"
                  :items-per-page="5"
                  loading-text="Loading... Please wait"
                  dense
                  :footer-props="{itemsPerPageOptions: [12]}"
                >
                  <template #item.spent="{ value }">
                    <span>{{`${user.displayCurrency} ${value.toFixed(2)}`}}</span>
                  </template>
                </v-data-table>
              </v-flex>
              <v-flex xs12 md6 style="min-height:340px; height=100%">
                <PieChart
                  :theme="theme"
                  :height="100"
                  :pieRadius="80"
                  :centerY="50"
                  :seriesData="categoryChartData"
                />
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
import PieChart from "@/components/Charts/PieChart";
import forEach from "lodash/forEach";
import { mapState } from "vuex";

export default {
  props: {
    years: {
      type: Array,
      default() {
        return [];
      }
    },
    months: {
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
    PieChart
  },
  mounted() {
    this.loaddata(this.selectedYear, this.selectedMonth);
  },
  methods: {
    loaddata(year, month) {
      this.loadyearlydata(year, month).then(() => {
        this.loadchart();
      });
    },
    loadyearlydata(year, month) {
      return Api.get(
        `/statistics/getcategoriesbreakdownforyear/${this.selectedYear}/${this.selectedMonth}`
      ).then(response => {
        this.categoryBreakdown = response.data;
      });
    },
    loadchart() {
      this.categoryChartData = [];
      forEach(this.categoryBreakdown, (value, key) => {
        this.categoryChartData.push({
          value: value.spent.toFixed(2),
          name: value.name,
          itemStyle: { color: value.colour }
        });
      });
    }
  },
  computed: {
    ...mapState({
      user: state => state.account.user
    })
  },
  data: () => ({
    selectedYear: new Date().getFullYear(),
    selectedMonth: "",
    categoryBreakdown: [],
    categoryChartData: [],
    headers: [
      { text: "Category", value: "name" },
      { text: "Budget (cumulative)", value: "budget" },
      { text: "Spent", value: "spent" }
    ]
  })
};
</script>