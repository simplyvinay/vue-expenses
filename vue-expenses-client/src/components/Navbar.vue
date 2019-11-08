<template>
  <div>
    <v-navigation-drawer v-model="sidebar" app left offset-y>
      <v-list-item v-for="item in menuItems" :key="item.text" :to="item.route" active-class="blue--text">
        <v-list-item-icon class="mr-2">
          <v-icon small>{{ item.icon }}</v-icon>
        </v-list-item-icon>
        <v-list-tile-content>{{ item.text }}</v-list-tile-content>
      </v-list-item>
    </v-navigation-drawer>

    <v-toolbar app flat dark color="primary">
      <span class="hidden-md-and-up">
        <v-app-bar-nav-icon @click="sidebar = !sidebar"></v-app-bar-nav-icon>
      </span>
      <v-spacer class="hidden-md-and-up"></v-spacer>
      <v-toolbar-title class="text-uppercase">
        <router-link to="/" tag="span" style="cursor: pointer" class="headline">
          <span class="font-weight-light">Vue</span>
          <span class="font-weight-bold">{{ appTitle }}</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn
          text
          v-for="item in menuItems"
          :key="item.text"
          :to="item.route"
          font-weight-thin
          class="body-2"
        >{{ item.text }}</v-btn>
      </v-toolbar-items>
      <!-- menu -->
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-avatar size="34px" color="blue lighten-2" class="ml-2" style="cursor: pointer">
            <span class="white--text" v-on="on">CJ</span>
          </v-avatar>          
        </template>
        <v-list>
          <v-list-item v-for="item in profileItems" :key="item" router dense :to="item.route">
            <v-list-item-icon class="mr-2">
              <v-icon small>{{ item.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-title>{{ item.text }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
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
        { icon: "dashboard", text: "Dashboard", route: "/dashboard" },
        { icon: "shopping_cart", text: "Expenses", route: "/expenses" }
      ],
      profileItems: [
        { icon: "settings", text: "Settings", route: "/settings" },
        { icon: "exit_to_app", text: "Sign Out", route: "/login" }
      ]
    };
  }
};
</script>

<style scoped>
::v-deep .v-toolbar__content{
  max-width: 1185px;
  margin: auto;
}
</style>