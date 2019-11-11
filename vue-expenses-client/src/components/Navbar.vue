<template>
  <div>
    <v-navigation-drawer v-model="sidebar" app left offset-y>
      <v-list-item
        v-for="item in navItems"
        :key="item.text"
        :to="item.route"
        active-class="blue--text"
      >
        <v-list-item-icon class="mr-2">
          <v-icon small>{{ item.icon }}</v-icon>
        </v-list-item-icon>
        <v-list-item-title>{{ item.text }}</v-list-item-title>
      </v-list-item>
    </v-navigation-drawer>

    <v-toolbar dense dark color="primary">
      <span class="hidden-md-and-up">
        <v-app-bar-nav-icon @click="sidebar = !sidebar"></v-app-bar-nav-icon>
      </span>
      <v-spacer class="hidden-md-and-up"></v-spacer>
      <v-toolbar-title class="text-uppercase">
        <router-link to="/dashboard" tag="span" style="cursor: pointer" class="headline">
          <span class="grey--text text--lighten-1">Vue</span>
          <span class="font-weight-bold">{{ appTitle }}</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <!-- menu -->
      <v-menu offset-y>
        <template v-slot:activator="{ on }" class="ml-4">
          <div v-on="on" class="d-flex align-center">
            <v-avatar size="30px" color="blue lighten-2" class="ml-2" style="cursor: pointer">
              <span class="white--text">{{userNameInitials}}</span>
            </v-avatar>
            <div class="hidden-sm-and-down">
              <v-btn text class="pa-1">
                <span class="text-capitalize">{{userName}}</span>
                <v-icon>expand_more</v-icon>
              </v-btn>
            </div>
          </div>
        </template>
        <v-list>
          <v-list-item v-for="item in menuItems" :key="item.text" router dense :to="item.route">
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
        <v-btn text v-for="item in navItems" :key="item.text" :to="item.route">
          <span>
            <v-icon small class="blue--text mr-1">{{ item.icon }}</v-icon>
          </span>
          <span class="mt-1 subtitle-2 text-capitalize">{{ item.text }}</span>
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
  </div>
</template>

<script>
export default {
  computed: {
    userNameInitials() {
      var initials = this.userName.match(/\b\w/g) || [];
      return ((initials.shift() || "") + (initials.pop() || "")).toUpperCase();
    }
  },
  data() {
    return {
      userName: "John Doe",
      appTitle: "Expenses",
      sidebar: false,
      navItems: [
        { icon: "dashboard", text: "Dashboard", route: "/dashboard" },
        { icon: "shopping_cart", text: "Expenses", route: "/expenses" },
        { icon: "insert_chart_outlined", text: "Stats", route: "/stats" },
        { icon: "settings", text: "Settings", route: "/settings" }
      ],
      menuItems: [
        { icon: "perm_identity", text: "Profile", route: "/profile" },
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

::v-deep .v-toolbar__items > a.v-btn--active:after {
  content: "";
  position: absolute;
  left: 18px;
  bottom: 0;
  height: 2px;
  width: 75%;
  border-bottom: 1px solid #1e88e5;
}

::v-deep .secondary-toolbar .v-toolbar__content {
  padding: 0;
}

::v-deep .v-btn:before {
  background-color: transparent !important;
}
</style>