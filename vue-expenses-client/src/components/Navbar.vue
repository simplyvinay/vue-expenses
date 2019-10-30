<template>
  <v-container fluid pa-0 d-flex justify-space-between>
    <div style="width: 75%">
      <v-toolbar class="grey lighten-4 elevation-0">
        <v-layout row justify-start align-center pl-6>
          <v-icon x-large color="blue">credit_card</v-icon>
          <div class="ml-2 title grey--text text--darken-3">Expenser</div>
        </v-layout>
        <v-spacer></v-spacer>
        <v-toolbar-items class="pa-3">
          <v-btn
            class="subtitle-2 font-weight-regular"
            text
            flat
            outline
            v-for="link in links"
            :key="link.text"
            router
            :to="link.route"
          >
            <span
              text
              flat
              font-weight-thin
              class="grey--text text--darken-3 text-capitalize"
            >{{link.text}}</span>
          </v-btn>
        </v-toolbar-items>
      </v-toolbar>
    </div>
    <div style="width: 25%">
      <v-toolbar class="elevation-0">
        <v-spacer></v-spacer>
        <v-btn flat text>
          <span>Sign Out</span>
          <v-icon right>exit_to_app</v-icon>
        </v-btn>
      </v-toolbar>
    </div>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      links: [
        { icon: "overview", text: "Overview", route: "/" },
        { icon: "shopping_cart", text: "Expenses", route: "/expenses" },
        { icon: "insert_chart", text: "Reports", route: "/reports" },
        { icon: "settings", text: "Settings", route: "/config" }
      ]
    };
  }
};
</script>

