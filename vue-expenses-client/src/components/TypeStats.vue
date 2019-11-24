<template>
  <v-container pb-0>
    <v-layout row wrap>
      <v-flex xs12 md12>
        <v-card class="pa-2 mr-2" tile>
          <div class="d-flex align-center">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Types Breakdown</div>
            <div class="ml-2">
              <v-select :items="years" dense label="Year" v-model="selectedYear" @change="loaddata"></v-select>
            </div>
            <div class="ml-2">
              <v-select
                :items="months"
                dense
                label="Month"
                item-text="name"
                item-value="value"
                v-model="selectedMonth"
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
                  :items="typesBreakdown"
                  sort-by="name"
                  :items-per-page="5"
                  loading-text="Loading... Please wait"
                  :footer-props="{itemsPerPageOptions: [5]}"
                >
                <template #item.spent="{ value }">
                    <span>{{value.toFixed(2)}}</span>
                  </template>
                </v-data-table>
              </v-flex>
              <v-flex xs12 md6 style="min-height:340px; height=100%">
                <PieChart :theme="theme" :height="100" :pieRadius="80" :centerY="50" :seriesData="typesChartData" />
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
import PieChart from "@/components/PieChart";
import forEach from "lodash/forEach";

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
    }
  },
  components: {
    PieChart
  },
  mounted() {
    this.loaddata(this.selectedYear, this.selectedMonth);
    this.theme = this.$vuetify.theme.dark ? "dark" : "";
  },
  methods: {
      loaddata(year, month) {
      this.loadyearlydata(year, month).then(() => {
        this.loadchart();
      });
    },
    loadyearlydata(year, month) {
      return Api.get(`/statistics/gettypesbreakdownforyear/${this.selectedYear}/${this.selectedMonth}`).then(
        response => {
          this.typesBreakdown = response.data;
        }
      );
    },
    loadchart() {
        this.typesChartData = [];
        forEach(this.typesBreakdown, (value, key) => {
            this.typesChartData.push({
                value: value.spent.toFixed(2),
                name: value.name
            })
        });
    }
  },
  data: () => ({
    theme: "",
    selectedYear: new Date().getFullYear(),
    selectedMonth: { name: "All", value: null },
    typesBreakdown: [],
    typesChartData: [],
    headers: [
      { text: "Type", value: "name" },
      { text: "Spent", value: "spent" }
    ]
  })
};
</script>