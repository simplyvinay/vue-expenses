<template>
  <chart :options="options" :style="style" :theme="theme" />
</template>

<script>
export default {
  props: {
    theme: {
      type: String
    },
    height: {
      type: Number,
      default: 100
    },
    seriesData: {
      type: Object,
      default() {
        return {
          xAxisData: [],
          legendData: [],
          data: []
        };
      }
    }
  },
  computed: {
    options() {
      return {
        backgroundColor: this.$vuetify.theme.dark ? "#424242" : "",
        textStyle: {
          fontFamily: "Nunito"
        },
        grid: {
          top: 20,
          bottom: 40,
          left: 60,
          right: 10
        },
        xAxis: {
          type: "category",
          data: this.seriesData.xAxisData // ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug"]
        },
        yAxis: {
          type: "value",
          splitLine: {
            show: false
          }
        },
        series: this.seriesData.data 
        //   [{
        //     name: "General",
        //     type: "bar",
        //     stack: "stack",
        //     barWidth: "45%",
        //     label: {
        //       normal: {
        //         show: true
        //       }
        //     },
        //     itemStyle: { color: "green" },
        //     data: [320, 302, 301, 334, 390, 330, 320]
        //   }]
        ,
        tooltip: {
          trigger: "axis",
          textStyle: {
            fontSize: 12,
            fontWeight: "normal"
          },
          axisPointer: {
            type: "shadow"
          }
        },
        legend: {
          data: this.seriesData.legendData // ["General", "Travel", "Shopping", "Utilities", "Misc"]
        }
      };
    }
  },
  data() {
    return {
      style: {
        height: this.height + "%",
        width: "100%"
      }
    };
  }
};
</script>

<style>
</style>