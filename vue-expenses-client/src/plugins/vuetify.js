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
        success: colors.green.lighten1,
        info: colors.blue.lighten1,
        error: colors.red.lighten1,
        background: colors.grey.lighten3
      },
      dark: {
        primary: '#2779bd',
        success: colors.green.lighten1,
        info: colors.blue.lighten1,
        error: colors.red.lighten1
      },
    },
  }
});