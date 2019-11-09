<template>
  <div>
    <v-navigation-drawer v-model="sidebar" app left offset-y>
      <v-list-item
        v-for="item in menuItems"
        :key="item.text"
        :to="item.route"
        active-class="blue--text"
      >
        <v-list-item-icon class="mr-2">
          <v-icon small>{{ item.icon }}</v-icon>
        </v-list-item-icon>
        <v-list-tile-content>{{ item.text }}</v-list-tile-content>
      </v-list-item>
    </v-navigation-drawer>

    <v-toolbar app flat dense dark color="primary">
      <span class="hidden-md-and-up">
        <v-app-bar-nav-icon @click="sidebar = !sidebar"></v-app-bar-nav-icon>
      </span>
      <v-spacer class="hidden-md-and-up"></v-spacer>
      <v-toolbar-title class="text-uppercase">
        <router-link to="/" tag="span" style="cursor: pointer" class="headline">
          <span class="grey--text text--lighten-1">Vue</span>
          <span class="font-weight-bold">{{ appTitle }}</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <!-- menu -->
      <v-menu offset-y >
        <template v-slot:activator="{ on }" class="ml-4">
          <v-avatar size="34px" color="blue lighten-2" class="ml-2" style="cursor: pointer" v-on="on">
            <span class="white--text">CJ</span>
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
    <v-toolbar dense flat class="hidden-sm-and-down secondary-toolbar">
      <v-toolbar-items>
        <v-btn
          text
          v-for="item in menuItems"
          :key="item.text"
          :to="item.route"
        >
          <span>
            <v-icon small class="blue--text mr-1">{{ item.icon }}</v-icon>
          </span>
          <span class="mt-1 body-2">
          {{ item.text }}
          </span>
        </v-btn>
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
        { icon: "dashboard", text: "Dashboard", route: "/dashboard" },
        { icon: "shopping_cart", text: "Expenses", route: "/expenses" },
        { icon: "insert_chart_outlined", text: "Stats", route: "/stats" },
        { icon: "settings", text: "Settings", route: "/settings" },
      ],
      profileItems: [
        { icon: "perm_identity", text: "Profile", route: "/settings" },
        { icon: "exit_to_app", text: "Sign Out", route: "/login" }
      ]
    };
  }
};
</script>

<style scoped lang="scss">
::v-deep .v-toolbar__content {
  max-width: 1185px;
  margin: auto;
}

::v-deep .v-toolbar__items>a.v-btn--active:after {
  content : "";
  position: absolute;
  left    : 18px;
  bottom  : 0;
  height  : 2px;
  width   : 75%;
  border-bottom:1px solid #1E88E5;
}

::v-deep .secondary-toolbar .v-toolbar__content {
  padding: 0;
}

::v-deep .v-btn:before {
  background-color: transparent !important;
}

</style>