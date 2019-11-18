<template>
  <chart :options="options" style="width: 100%" :theme="theme" />
</template>

<script>
export default {
  props: {
    theme: {
      type: String
    },
    seriesData: {
      type: Object,
      default: function() {
        return {
          xAxisData: [],
          data: []
        };
      }
    }
  },
  data() {
    debugger;
    return {
      options: {
        backgroundColor: this.$vuetify.theme.dark ? "#424242" : "",
        textStyle: {
          fontFamily: "Nunito"
        },
        grid: {
          show: false,
          top: 80,
          bottom: 80
        },
        xAxis: {
          data: this.seriesData.xAxisData
        },
        yAxis: [
          {
            type: "value",
            splitLine: {
              show: false
            }
          }
        ],
        series: [
          {
            type: "bar",
            data: this.seriesData.data,
            barWidth: "50%",
            center: ["50%", "50%"],
            label: {
              normal: {
                show: true,
                // position: "inside",
                // rotate: 90
                position: "top"
              }
            },
            itemStyle: {
              normal: {},
              emphasis: {
                barBorderWidth: 1,
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowOffsetY: 0,
                shadowColor: "rgba(0,0,0,0.8)"
              }
            }
          }
        ],
        title: {
          text: "Expenses By Month",
          x: "center",
          top: "20",
          textStyle: {
            fontSize: 14,
            fontWeight: "normal"
          }
        },
        //color: ["#2196f3"],
        tooltip: {
          trigger: "item",
          textStyle: {
            fontSize: 12,
            fontWeight: "normal"
          }
        }
      }
    };
  }
};
</script>

<style>
</style>