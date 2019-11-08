import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors'

Vue.use(Vuetify);

export default new Vuetify({
  icons: {
    iconfont: 'mdi',
  },
  theme: {
    dark: false,
    themes: {
      light: {
        primary: '#2779bd',
        //secondary: colors.grey.darken3,
        background: colors.grey.lighten4
      },
      dark: {
        primary: '#424242',
        //secondary: colors.grey.lighten2,
      },
    },
  }
});