<template>
  <div>
    <v-navigation-drawer v-model="sidebar" app right offset-y>
      <v-list-item
        v-for="item in menuItems"
        :key="item.text"
        :to="item.route"
        @click.native.prevent="handleRoute(item)"
      >
        <v-list-item-icon>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-icon>
        <v-list-tile-content>{{ item.text }}</v-list-tile-content>
      </v-list-item>
    </v-navigation-drawer>

    <v-toolbar app flat dense>
      <v-toolbar-title class="text-uppercase">
        <router-link to="/" tag="span" style="cursor: pointer" class="headline">
          <span class="font-weight-light grey--text">Vue</span>
          <span class="font-weight-bold blue--text">{{ appTitle }}</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <span class="hidden-md-and-up">
        <v-app-bar-nav-icon @click="sidebar = !sidebar"></v-app-bar-nav-icon>
      </span>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn
          text
          dense
          v-for="item in menuItems"
          :key="item.text"
          :to="item.route"
          font-weight-thin
          class="text-capitalize"
          @click.native.prevent="handleRoute(item)"
        >{{ item.text }}</v-btn>
      </v-toolbar-items>
    </v-toolbar>
  </div>
</template>

<script>
export default {
  data() {
    return {
      appTitle: "Expenses",
      sidebar: false,
      menuItems: [
        { icon: "dashboard", text: "Dashboard", route: "/" },
        { icon: "shopping_cart", text: "Expenses", route: "/expenses" },
        { icon: "settings", text: "Settings", route: "/config" },
        { icon: "exit_to_app", text: "Sign Out", route: "/signout" }
      ]
    };
  },
  methods: {
    handleRoute(item) {
      if (item.route === "/signout") {
        alert("Singed out");
      } else {
        this.$router.push(item.route);
      }
    }
  }
};
</script>

<style>
</style>