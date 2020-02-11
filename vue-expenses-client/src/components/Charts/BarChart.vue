<template>
  <chart :options="options" :style="style" :theme="theme" />
</template>

<script>
export default {
  props: {
    showTitle: {
      type: Boolean,
      default: true
    },
    titleText: {
      type: String,
      default: ""
    },
    theme: {
      type: String
    },
    height: {
      type: Number,
      default: 100
    },
    barWidth: {
      type: Number,
      default: 45
    },
    seriesData: {
      type: Object,
      default() {
        return {
          xAxisData: [],
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
          show: false,
          top: 80,
          bottom: 40,
          left: 60,
          right: 10
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
            barWidth: this.barWidth + "%",
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
          text: this.titleText,
          x: "center",
          top: "20",
          textStyle: {
            fontSize: 14,
            fontWeight: "normal"
          },
          show: this.showTitle
        },
        //color: ["#2196f3"],
        tooltip: {
          trigger: "item",
          textStyle: {
            fontSize: 12,
            fontWeight: "normal"
          }
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