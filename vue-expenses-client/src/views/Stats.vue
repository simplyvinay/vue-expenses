<template>
  <div>
    <v-container>
      <v-layout row>
        <v-flex xs12 sm6 md3>
          <v-card class="pa-4 mr-2" outlined>
            <div class="text-overline mb-2">Overall Spent</div>
            <div class="text-h6 blue--text text-capitalize">{{overallSpent}}</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.xs, 'pa-4 mr-2': $vuetify.breakpoint.smAndUp}"
            outlined
          >
            <div class="text-overline mb-2">Most Spent By</div>
            <div class="text-h6 blue--text text-capitalize">{{mostSpentBy}}</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.smAndDown, 'pa-4 mr-2': $vuetify.breakpoint.mdAndUp}"
            outlined
          >
            <div class="text-overline mb-2">Most Spent On</div>
            <div class="text-h6 blue--text text-capitalize">{{mostSpentOn}}</div>
          </v-card>
        </v-flex>
        <v-flex xs12 sm6 md3>
          <v-card
            :class="{'pa-4 mt-2 mr-2': $vuetify.breakpoint.xs, 'pa-4 mr-2 mt-2': $vuetify.breakpoint.sm, 'pa-4 mr-2': $vuetify.breakpoint.mdAndUp}"
            outlined
          >
            <div class="text-overline mb-2">Spent This Year</div>
            <div class="text-h6 blue--text text-capitalize">{{spentThisYear}}</div>
          </v-card>
        </v-flex>
        <v-flex xs12 md12 my-3>
          <v-flex>
            <ExpensesStats :years="getYears()" :theme="theme" />
          </v-flex>
        </v-flex>
        <v-flex xs12 md12 my-3>
          <v-flex>
            <ExpenseCategoryStats :years="getYears()" :months="months" :theme="theme" />
          </v-flex>
        </v-flex>
        <v-flex xs12 md12 my-3>
          <v-flex>
            <ExpenseTypeStats :years="getYears()" :months="months" :theme="theme" />
          </v-flex>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import ExpensesStats from "@/components/ExpensesStats";
import ExpenseCategoryStats from "@/components/ExpenseCategoryStats";
import ExpenseTypeStats from "@/components/ExpenseTypeStats";
import { mapState, mapGetters } from "vuex";

export default {
  components: { ExpensesStats, ExpenseCategoryStats, ExpenseTypeStats },
  methods: {
    getYears() {
      var items = [];
      var startYear = new Date().getFullYear() - 4;
      for (var i = 0; i < 7; i++) {
        items.push(startYear++);
      }
      return items;
    }
  },
  computed: {
    ...mapGetters("expenses", [
      "overallSpent",
      "mostSpentBy",
      "mostSpentOn",
      "spentThisYear"
    ]),
    ...mapState({
      theme: state => (state.account.user ? state.account.user.theme : "")
    })
  },
  data: () => ({
    months: [
      { name: "All", value: "" },
      { name: "January", value: 1 },
      { name: "February", value: 2 },
      { name: "March", value: 3 },
      { name: "April", value: 4 },
      { name: "May", value: 5 },
      { name: "June", value: 6 },
      { name: "July", value: 7 },
      { name: "August", value: 8 },
      { name: "September", value: 9 },
      { name: "October", value: 10 },
      { name: "November", value: 11 },
      { name: "December", value: 12 }
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