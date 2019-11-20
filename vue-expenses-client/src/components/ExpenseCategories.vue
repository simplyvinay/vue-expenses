<template>
  <v-data-table
    :headers="headers"
    :items="categories"
    sort-by="name"
    :items-per-page="5"
    loading-text="Loading... Please wait"
  >
    <template v-slot:top>
      <div class="d-flex align-center pa-1 pb-2">
        <span class="blue--text font-weight-medium">Categories</span>
        <v-divider class="mx-2 my-1" inset vertical style="height: 20px"></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn outlined small class="blue--text font-weight-bold" v-on="on">New Category</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ categoryFormTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <input type="hidden" v-model="editedCategory.id" />
                <v-text-field
                  class="ma-0 pa-0 form-label"
                  dense
                  v-model="editedCategory.name"
                  label="Category Name"
                ></v-text-field>
                <v-textarea
                  class="ma-0 pa-0 form-label"
                  dense
                  counter="100"
                  rows="2"
                  v-model="editedCategory.description"
                  label="Description"
                ></v-textarea>
                <v-text-field
                  class="ma-0 pa-0 form-label"
                  dense
                  v-model="editedCategory.budget"
                  label="Monthly Budget"
                ></v-text-field>
                <v-text-field
                  class="ma-0 pa-0 form-label"
                  dense
                  v-model="editedCategory.colourHex"
                  label="Colour"
                >
                  <template v-slot:append>
                    <v-menu
                      v-model="menu"
                      top
                      nudge-bottom="110"
                      nudge-left="20"
                      :close-on-content-click="false"
                    >
                      <template v-slot:activator="{ on }">
                        <div :style="swatchStyle(editedCategory)" v-on="on" />
                      </template>
                      <v-card>
                        <v-card-text class="pa-0">
                          <v-color-picker
                            mode="hexa"
                            hide-mode-switch
                            v-model="editedCategory.colourHex"
                            flat
                          />
                        </v-card-text>
                        <v-card-actions class="pa-0 pb-1 pr-1">
                          <v-spacer></v-spacer>
                          <v-btn
                            outlined
                            small
                            class="blue--text font-weight-bold"
                            @click="menu = false"
                          >Select</v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-menu>
                  </template>
                </v-text-field>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                outlined
                small
                class="blue--text font-weight-bold"
                @click="saveCategory"
                :loading="loading"
              >Save</v-btn>
              <v-btn outlined small class="blue--text font-weight-bold" @click="close">Cancel</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </template>
    <template v-slot:item.colourHex="{ item }">
      <v-chip
        :color="item.colourHex"
        style="padding: 0px; height: 20px; width: 20px"
        flat
        small
        class="ml-1 mb-1"
      ></v-chip>
    </template>
    <template v-slot:item.action="{ item }">
      <v-icon small class="mr-2" @click="editCategory(item)">edit</v-icon>
      <v-icon small @click="deleteCategory(item)">delete</v-icon>
    </template>
    <template v-slot:no-data>
      <span>No Data Available</span>
    </template>
  </v-data-table>
</template>

<script>
import { mapState, mapActions } from "vuex";
import {
  CREATE_CATEGORY,
  EDIT_CATEGORY,
  REMOVE_CATEGORY
} from "@/store/_actiontypes";

export default {
  data: () => ({
    loading: false,
    dialog: false,
    menu: false,
    headers: [
      { text: "Id", value: "id", align: " d-none" },
      { text: "Name", value: "name" },
      { text: "Description", value: "description" },
      { text: "Budget", value: "budget", width: 100 },
      { text: "Colour", value: "colourHex", width: 100 },
      { text: "Actions", value: "action", sortable: false, width: 50 }
    ],
    editedCategory: {
      id: 0,
      name: "",
      description: "",
      budget: 0,
      colourHex: "#1976D2FF"
    },
    defaultCategory: {
      id: 0,
      name: "",
      description: "",
      budget: 0,
      colourHex: "#1976D2FF"
    }
  }),

  computed: {
    ...mapState({ categories: state => state.expenseCategories.categories }),
    categoryFormTitle() {
      return this.editedCategory.id === 0 ? "New Category" : "Edit Category";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },
  methods: {
    ...mapActions("expenseCategories", [
      CREATE_CATEGORY,
      EDIT_CATEGORY,
      REMOVE_CATEGORY
    ]),

    swatchStyle(item) {
      const { colourHex } = item;
      return {
        backgroundColor: colourHex,
        cursor: "pointer",
        height: "20px",
        width: "20px",
        borderRadius: "50%"
      };
    },

    editCategory(item) {
      this.editedCategory = Object.assign({}, item);
      this.dialog = true;
    },

    deleteCategory(item) {
      confirm("Are you sure you want to delete this item?") &&
        this.REMOVE_CATEGORY({ id: item.id });
    },

    close() {
      this.dialog = false;
      this.editedCategory = Object.assign({}, this.defaultCategory);
    },

    saveCategory() {
      this.loading = true;
      var expenseCategory = this.editedCategory;
      if (expenseCategory.id == 0) {
        this.CREATE_CATEGORY({
          expenseCategory
        })
          .then(() => {
            this.close();
            this.loading = false;
          })
          .catch(() => {
            this.loading = false;
          });
      } else {
        this.EDIT_CATEGORY({
          expenseCategory
        })
          .then(() => {
            this.close();
            this.loading = false;
          })
          .catch(() => {
            this.loading = false;
          });
      }
    }
  }
};
</script>

<style>
</style>