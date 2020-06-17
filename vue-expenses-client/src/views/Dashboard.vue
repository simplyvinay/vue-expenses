<template>
  <div>
    <v-container>
      <v-layout row justify-space-between>
        <v-flex xs12 md6>
          <v-card class="pa-2 mr-2" raised min-height="350px">
            <div class="blue--text px-2 py-1 text-capitalize font-weight-medium">Add New Expense</div>
            <v-divider></v-divider>
            <ExpenseForm
              :expense="expense"
              :onSubmitClick="saveExpense"
              :loading="loading"
              ref="form"
            />
          </v-card>
        </v-flex>
        <v-flex xs12 md6>
          <v-card
            :class="{'pa-2 mr-2 mt-2': $vuetify.breakpoint.smAndDown, 'pa-2 mr-2': $vuetify.breakpoint.mdAndUp}"
            tile
            min-height="340px"
            height="100%"
          >
            <div
              class="blue--text px-2 py-1 text-capitalize font-weight-medium"
            >Budget (Current Month)</div>
            <v-divider></v-divider>
            <DoughnutChart
              :height="75"
              :theme="user.theme"
              :showLabel="true"
              :showLabelLines="true"
              :seriesData="monthlyBudget.data"
              :centerY="50"
              :pieRadiusOuter="75"
            />
            <div class="d-flex justify-space-around text-subtitle-2 px-12 mx-12">
              <div>
                <div>Limit</div>
                <div>{{`${user.displayCurrency} ${monthlyBudget.totalBudget}`}}</div>
              </div>
              <div>
                <div>Spent</div>
                <div>{{`${user.displayCurrency} ${monthlyBudget.totalSpent}`}}</div>
              </div>
            </div>
          </v-card>
        </v-flex>
        <v-flex xs12 md12>
          <v-container px-0 pb-0>
            <v-card class="pa-2 mr-2" tile>
              <div
                class="blue--text px-2 py-1 text-capitalize font-weight-medium"
              >Budgets By Categories (Current Month)</div>
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
                    :theme="user.theme"
                    :showTooltip="false"
                    :seriesData="budget.monthlyBudget"
                  />
                </div>
              </div>
            </v-card>
          </v-container>
        </v-flex>
        <v-flex xs12 md12>
          <v-flex>
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
                        <v-flex xs12 md6 style="min-height:340px;height=100%">
                          <BarChart
                            :theme="user.theme"
                            titleText="Expenses"
                            :seriesData="yearlyExpenses"
                          />
                        </v-flex>
                        <v-flex xs12 md6 style="min-height:340px;height=100%">
                          <PieChart
                            :theme="user.theme"
                            titleText="Category"
                            :seriesData="categoryExpenses"
                          />
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
import ExpenseForm from "@/components/ExpenseForm";
import DoughnutChart from "@/components/Charts/DoughnutChart";
import BarChart from "@/components/Charts/BarChart";
import PieChart from "@/components/Charts/PieChart";
import { mapState, mapActions, mapGetters } from "vuex";
import { CREATE_EXPENSE } from "@/store/_actiontypes";

export default {
  components: { ExpenseForm, DoughnutChart, BarChart, PieChart },
  computed: {
    ...mapGetters("statistics", [
      "monthlyBudget",
      "monthlyBudgetsByCategory",
      "yearlyExpenses",
      "categoryExpenses"
    ]),
    ...mapState({
      user: state => state.account.user
    })
  },
  data() {
    return {
      loading: false,
      dateMenu: false,
      expense: {}
    };
  },
  methods: {
    ...mapActions("expenses", [CREATE_EXPENSE]),
    saveExpense() {
      this.loading = true;
      this.CREATE_EXPENSE({
        expense: this.expense
      })
        .then(() => {
          this.expense = {};
          this.$refs.form.reset();
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style scoped>
.category-budgets {
  display: flex;
  justify-content: space-between;
  overflow: hidden;
  height: 180px;
  padding-left: 10px;
}
.category-budgets:hover {
  overflow-x: scroll;
}
.category-budgets-budget {
  width: 180px;
  flex: 0 0 auto;
}
</style>