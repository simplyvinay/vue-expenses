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
    pieRadius: {
      type: Number,
      default: 60
    },
    centerX: {
      type: Number,
      default: 50
    },
    centerY: {
      type: Number,
      default: 60
    },
    seriesData: {
      type: Array,
      default() {
        return [{ value: 310, name: "General", itemStyle: { color: "red" } }];
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
        series: [
          {
            name: "Category",
            type: "pie",
            radius: this.pieRadius + "%",
            center: [this.centerX + "%", this.centerY + "%"],
            roseType: "radius",
            data: this.seriesData,
            itemStyle: {
              emphasis: {
                show: true,
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: "rgba(0, 0, 0, 0.5)"
              }
            },
            label: {
              normal: {
                //position: "inner",
                show: true,
                //fontWeight: "bolder",
                fontSize: "14"
              }
            },
            labelLine: {
              normal: {
                show: true,
                length: 0
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
            fontWeight: "norm,al"
          },
          show: this.showTitle
        },
        legend: {
          show: false,
          orient: "vertical",
          left: "left"
        },
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b} : {c}<br/>({d}%)",
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