<template>
  <div>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12 md6>
          <v-card class="pa-2 mr-2" raised>
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Add New Expense</div>
            <v-divider></v-divider>
            <ExpenseForm />
          </v-card>
        </v-flex>
        <v-flex xs12 md6>
          <v-card
            :class="{'pa-2 mr-2 mt-2': $vuetify.breakpoint.smAndDown, 'pa-2 mr-2': $vuetify.breakpoint.mdAndUp}"
            tile
            min-height="340px"
            height="100%"
          >
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Monthly Budget</div>
            <v-divider></v-divider>
            <DoughnutChart
              titleText="This Month"
              :height="70"
              :showTitle="true"
              :titleFontSize="15"
              :theme="theme"
              :showLabel="true"
              :showLabelLines="true"
              :seriesData="monthlyBudget"
            />
            <div class="d-flex justify-space-around subtitle-2">
              <div>
                <div>Monthly Limit</div>
                <div>35000</div>
              </div>
              <div>
                <div>Spent Amount</div>
                <div>20000</div>
              </div>
            </div>
          </v-card>
        </v-flex>
        <v-flex xs12 md12>
          <v-container px-0 pb-0>
            <v-card class="pa-2 mr-2" tile>
              <div
                class="blue--text px-2 py-1 text-capitalize font-weight-medium"
              >Budgets By Categories</div>
              <v-divider></v-divider>
              <div class="category-budgets">
                <div
                  v-for="budget in monthlyBudgetsByCategory"
                  :key="budget.name"
                  class="category-budgets-budget"
                >
                  <DoughnutChart
                    :titleText="budget.name"
                    :showTitle="true"
                    :height="90"
                    :titleFontSize="14"
                    :theme="theme"
                    :showTooltip="false"
                    :seriesData="budget.monthlyBudget"
                  />
                </div>
              </div>
            </v-card>
          </v-container>
        </v-flex>
        <v-flex xs12 md12>
          <v-container>
            <v-layout row wrap>
              <v-flex xs12 md12>
                <v-card class="pa-2 mr-2" tile>
                  <div
                    class="blue--text px-2 py-1 text-capitalize font-weight-medium"
                  >Breakdown (Current Year)</div>
                  <v-divider></v-divider>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12 md6>
                        <BarChart :theme="theme" :seriesData="yearlyExpenses" />
                      </v-flex>
                      <v-flex xs12 md6>
                        <PieChart :theme="theme" :seriesData="categoryExpenses" />
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
import ExpenseForm from "@/components/ExpenseForm";
import DoughnutChart from "@/components/DoughnutChart";
import BarChart from "@/components/BarChart";
import PieChart from "@/components/PieChart";
import { mapGetters } from "vuex";

export default {
  components: { ExpenseForm, DoughnutChart, BarChart, PieChart },
  mounted() {
    this.theme = this.$vuetify.theme.dark ? "dark" : "";
  },
  computed: {
    ...mapGetters("statistics", [
      "monthlyBudget",
      "monthlyBudgetsByCategory",
      "yearlyExpenses",
      "categoryExpenses"
    ])
  },
  data() {
    return {
      dateMenu: false,
      theme: ""
    };
  }
};
</script>

<style scoped>
.category-budgets {
  display: flex;
  justify-content: space-evenly;
  overflow: hidden;
  height: 180px;
  padding-left: 40px;
}
.category-budgets:hover {
  overflow-x: scroll;
}
.category-budgets-budget {
  width: 200px;
  flex: 0 0 auto;
}
</style>