<template>
  <v-data-table :headers="headers" :items="categories" sort-by="calories" :items-per-page="5">
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
                  v-model="editedCategory.colour"
                  label="Colour"
                >
                  <template v-slot:append>
                    <v-menu v-model="menu" top nudge-bottom="110" nudge-left="20" :close-on-content-click="false">
                      <template v-slot:activator="{ on }">
                        <div :style="swatchStyle(editedCategory)" v-on="on" />
                      </template>
                      <v-card>
                        <v-card-text class="pa-0">
                          <v-color-picker
                            mode="hexa"
                            hide-mode-switch
                            v-model="editedCategory.colour"
                            flat
                          />
                        </v-card-text>
                        <v-card-actions class="pa-0 pb-1 pr-1">
                          <v-spacer></v-spacer>
                          <v-btn outlined small class="blue--text font-weight-bold" @click="menu = false">Select</v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-menu>
                  </template>
                </v-text-field>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn outlined small class="blue--text font-weight-bold" @click="saveCategory">Save</v-btn>
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
      <v-icon small class="mr-2" @click="editCategory(item)">edit</v-icon>
      <v-icon small @click="deleteCategory(item)">delete</v-icon>
    </template>
    <template v-slot:no-data>
      <span>No Data Available</span>
    </template>
  </v-data-table>
</template>

<script>
export default {
  data: () => ({
    dialog: false,
    menu: false,
    headers: [
      { text: "Name", value: "name" },
      { text: "Description", value: "description" },
      { text: "Budget", value: "budget", width: 100 },
      { text: "Colour", value: "colour", width: 100 },
      { text: "Actions", value: "action", sortable: false, width: 50 }
    ],
    categories: [],
    editedIndex: -1,
    editedCategory: {
      name: "",
      description: "",
      budget: 0,
      colour: "#1976D2FF"
    },
    defaultCategory: {
      name: "",
      description: "",
      budget: 0,
      colour: "#1976D2FF"
    }
  }),

  computed: {
    categoryFormTitle() {
      return this.editedIndex === -1 ? "New Category" : "Edit Category";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },

  created() {
    this.initialize();
  },

  methods: {
    swatchStyle(item) {
      const { colour } = item;
      return {
        backgroundColor: colour,
        cursor: "pointer",
        height: "20px",
        width: "20px",
        borderRadius: "50%"
      };
    },
    initialize() {
      this.categories = [
        {
          name: "General Expenses",
          description:
            "Velit quam sint distinctio. Provident sed vel neque qui. Asperiores dolorum voluptatum earum dolore.",
          budget: 2000,
          colour: "#1976D2FF"
        },
        {
          name: "Shopping",
          description: "",
          budget: 1500,
          colour: "#1976D2FF"
        }
      ];
    },

    editCategory(item) {
      this.editedIndex = this.categories.indexOf(item);
      this.editedCategory = Object.assign({}, item);
      this.dialog = true;
    },

    deleteCategory(item) {
      const index = this.categories.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.categories.splice(index, 1);
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedCategory = Object.assign({}, this.defaultCategory);
        this.editedIndex = -1;
      }, 300);
    },

    saveCategory() {
      if (this.editedIndex > -1) {
        Object.assign(this.categories[this.editedIndex], this.editedCategory);
      } else {
        this.categories.push(this.editedCategory);
      }
      this.close();
    }
  }
};
</script>

<style>
</style>