<template>
  <v-data-table
    :headers="headers"
    :items="types"
    sort-by="name"
    :items-per-page="5"
    loading-text="Loading... Please wait"
  >
    <template v-slot:top>
      <div class="d-flex align-center pa-1 pb-2">
        <span class="blue--text font-weight-medium">Types</span>
        <v-divider class="mx-2 my-1" inset vertical style="height: 20px"></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn outlined small class="blue--text font-weight-bold" v-on="on">New Type</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ categoryFormTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <input type="hidden" v-model="editedType.id" />
                <v-text-field
                  class="ma-0 pa-0 form-label"
                  dense
                  v-model="editedType.name"
                  label="Expense Type"
                ></v-text-field>
                <v-textarea
                  class="ma-0 pa-0 form-label"
                  dense
                  counter="100"
                  rows="2"
                  v-model="editedType.description"
                  label="Description"
                ></v-textarea>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                outlined
                small
                class="blue--text font-weight-bold"
                @click="saveType"
                :loading="loading"
              >Save</v-btn>
              <v-btn outlined small class="blue--text font-weight-bold" @click="close">Cancel</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </template>
    <template v-slot:item.colour="{ item }">
      <v-chip
        :color="item.colour"
        style="padding: 0px; height: 20px; width: 20px"
        flat
        small
        class="ml-1 mb-1"
      ></v-chip>
    </template>
    <template v-slot:item.action="{ item }">
      <v-icon small class="mr-2" @click="editType(item)">edit</v-icon>
      <v-icon small @click="deleteType(item)">delete</v-icon>
    </template>
    <template v-slot:no-data>
      <span>No Data Available</span>
    </template>
  </v-data-table>
</template>

<script>
import { mapState, mapActions } from "vuex";
import {
  CREATE_EXPENSE_TYPE,
  EDIT_EXPENSE_TYPE,
  REMOVE_EXPENSE_TYPE
} from "@/store/_actiontypes";

export default {
  data: () => ({
    loading: false,
    dialog: false,
    headers: [
      { text: "Id", value: "id", align: " d-none" },
      { text: "Name", value: "name" },
      { text: "Description", value: "description" },
      { text: "Actions", value: "action", sortable: false, width: 50 }
    ],
    editedType: {
      id: 0,
      name: "",
      description: ""
    },
    defaultType: {
      id: 0,
      name: "",
      description: ""
    }
  }),

  computed: {
    ...mapState({
      types: state => state.expenseTypes.types
    }),

    categoryFormTitle() {
      return this.editedType.id === 0
        ? "New Expense Type"
        : "Edit Expense Type";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },
  methods: {
    ...mapActions("expenseTypes", [
      CREATE_EXPENSE_TYPE,
      EDIT_EXPENSE_TYPE,
      REMOVE_EXPENSE_TYPE
    ]),

    editType(item) {
      this.editedType = Object.assign({}, item);
      this.dialog = true;
    },

    deleteType(item) {
      confirm("Are you sure you want to delete this item?") &&
        this.REMOVE_EXPENSE_TYPE({ id: item.id });
    },

    close() {
      this.dialog = false;
      this.editedType = Object.assign({}, this.defaultType);
    },

    saveType() {
      var expenseType = this.editedType;
      this.loading = true;
      if (expenseType.id == 0) {
        this.CREATE_EXPENSE_TYPE({
          expenseType
        })
          .then(() => {
            this.close();
          })
          .finally(() => {
            this.loading = false;
          });
      } else {
        this.EDIT_EXPENSE_TYPE({
          expenseType
        })
          .then(() => {
            this.close();
          })
          .finally(() => {
            this.loading = false;
          });
      }
    }
  }
};
</script>

<style>
</style>