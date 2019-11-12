<template>
  <v-data-table :headers="headers" :items="types" sort-by="calories" :items-per-page="5">
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
              <span class="headline">{{ categoryFormTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
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
              <v-btn outlined small class="blue--text font-weight-bold" @click="saveType">Save</v-btn>
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
  ADD_TYPE,
  EDIT_TYPE,
  REMOVE_TYPE,
  GET_TYPES
} from "@/store/_actionTypes";

export default {
  created() {
    this.$store.dispatch(`type/${GET_TYPES}`);
  },
  data: () => ({
    dialog: false,
    headers: [
      { text: "Name", value: "name" },
      { text: "Description", value: "description" },
      { text: "Actions", value: "action", sortable: false, width: 50 }
    ],
    editedIndex: -1,
    editedType: {
      name: "",
      description: ""
    },
    defaultType: {
      name: "",
      description: ""
    }
  }),

  computed: {
    ...mapState("type", ["types"]),
    categoryFormTitle() {
      return this.editedIndex === -1 ? "New Expense Type" : "Edit Expense Type";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },
  methods: {
    ...mapActions("type", [ADD_TYPE, EDIT_TYPE, REMOVE_TYPE]),

    editType(item) {
      this.editedIndex = this.types.indexOf(item);
      this.editedType = Object.assign({}, item);
      this.dialog = true;
    },

    deleteType(item) {
      const index = this.types.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.types.splice(index, 1);
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedType = Object.assign({}, this.defaultType);
        this.editedIndex = -1;
      }, 300);
    },

    saveType() {
      this.$store.dispatch(`type/${EDIT_TYPE}`, this.editedType);
      if (this.editedIndex > -1) {
        Object.assign(this.types[this.editedIndex], this.editedType);
      } else {
        this.types.push(this.editedType);
      }
      this.close();
    }
  }
};
</script>

<style>
</style>