<template>
  <div>
    <v-container>
      <v-layout row>
        <v-flex xs12 sm6 md3>
          <v-card class="pa-4 mr-2" outlined>
            <div class="overline mb-2">Overall Spent</div>
            <div class="title blue--text text-capitalize">8,000</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.xs, 'pa-4 mr-2': $vuetify.breakpoint.smAndUp}"
            outlined
          >
            <div class="overline mb-2">Most Spent By</div>
            <div class="title blue--text text-capitalize">Credit Card</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.smAndDown, 'pa-4 mr-2': $vuetify.breakpoint.mdAndUp}"
            outlined
          >
            <div class="overline mb-2">Most Spent On</div>
            <div class="title blue--text text-capitalize">General Expenses</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.xs, 'pa-4 mr-2 mt-2': $vuetify.breakpoint.sm, 'pa-4 mr-2': $vuetify.breakpoint.mdAndUp}"
            outlined
          >
            <div class="overline mb-2">Spent This Year</div>
            <div class="title blue--text text-capitalize">5,000</div>
          </v-card>
        </v-flex>
        <v-flex xs12 md12>
          <v-flex>
            <ExpensesStats :years="getYears(false)" />
          </v-flex>
        </v-flex>
        <v-flex xs12 md12>
          <v-flex>
            <v-container pb-0>
              <v-layout row wrap>
                <v-flex xs12 md12>
                  <v-card class="pa-2 mr-2" tile>
                    <div class="d-flex align-center">
                      <div
                        class="blue--text px-2 py-1 text-capitalize font-weight-medium"
                      >Category Breakdown</div>
                      <div class="ml-2">
                        <v-select :items="getYears(true)" dense label="Year"></v-select>
                      </div>
                      <div class="ml-2">
                        <v-select :items="months" dense label="Month"></v-select>
                      </div>
                    </div>
                    <v-divider></v-divider>
                    <v-container>
                      <v-layout row wrap>
                        <v-flex xs12 md6>
                          <v-data-table
                            :headers="headers"
                            :items="categories"
                            sort-by="name"
                            :items-per-page="5"
                            loading-text="Loading... Please wait"
                            :footer-props="{
                                itemsPerPageOptions: [5],
                              }"
                          />
                        </v-flex>
                        <v-flex xs12 md6 style="min-height:340px; height=100%">
                          <PieChart :theme="theme" :seriesData="categoryExpenses" />
                        </v-flex>
                      </v-layout>
                    </v-container>
                  </v-card>
                </v-flex>
              </v-layout>
            </v-container>
          </v-flex>
        </v-flex>
        <v-flex xs12 md12>
          <v-flex>
            <v-container pb-0>
              <v-layout row wrap>
                <v-flex xs12 md12>
                  <v-card class="pa-2 mr-2" tile>
                    <div class="d-flex align-center">
                      <div
                        class="blue--text px-2 py-1 text-capitalize font-weight-medium"
                      >Types Breakdown</div>
                      <div class="ml-2">
                        <v-select :items="getYears(true)" dense label="Year"></v-select>
                      </div>
                      <div class="ml-2">
                        <v-select :items="months" dense label="Month"></v-select>
                      </div>
                    </div>
                    <v-divider></v-divider>
                    <v-container>
                      <v-layout row wrap>
                        <v-flex xs12 md6>
                          <v-data-table
                            :headers="headers"
                            :items="categories"
                            sort-by="name"
                            :items-per-page="5"
                            loading-text="Loading... Please wait"
                            :footer-props="{
                                itemsPerPageOptions: [5],
                              }"
                          />
                        </v-flex>
                        <v-flex xs12 md6 style="min-height:340px; height=100%">
                          <PieChart :theme="theme" :seriesData="categoryExpenses" />
                        </v-flex>
                      </v-layout>
                    </v-container>
                  </v-card>
                </v-flex>
              </v-layout>
            </v-container>
          </v-flex>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import PieChart from "@/components/PieChart";
import BarChart from "@/components/BarChart";
import ExpensesStats from "@/components/ExpensesStats";
import { mapState, mapActions, mapGetters } from "vuex";

export default {
  components: { ExpensesStats, PieChart },
  computed: {
    ...mapState({ categories: state => state.expenseCategories.categories }),
    ...mapGetters("statistics", ["categoryExpenses", "yearlyExpenses"]),
    months: () => {
      return [
        "All",
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
      ];
    }
  },
  methods: {
    getYears(showAll) {
      var items = showAll ? ["All"] : [];
      var startYear = new Date().getFullYear() - 4;
      for (var i = 0; i < 7; i++) {
        items.push(startYear++);
      }
      return items;
    }
  },
  mounted() {
    this.theme = this.$vuetify.theme.dark ? "dark" : "";
  },
  data: () => ({
    theme: "",
    headers: [
      { text: "Id", value: "id", align: " d-none" },
      { text: "Name", value: "name" },
      { text: "Description", value: "description" },
      { text: "Budget", value: "budget", width: 100 },
      { text: "Colour", value: "colourHex", width: 100 },
      { text: "Actions", value: "action", sortable: false, width: 50 }
    ]
  })
};
</script>

<style scoped>
::v-deep .v-text-field__details {
  display: none;
}
::v-deep .v-data-footer .v-data-footer__select .v-input {
  margin: 0;
  margin-left: 10px;
}
::v-deep .v-select__slot {
  font-size: smaller;
  width: 100px;
}
::v-deep .v-text-field > .v-input__control > .v-input__slot:before {
  border: 0px;
}

::v-deep .v-select__slot input {
  width: 50px;
}
</style> 