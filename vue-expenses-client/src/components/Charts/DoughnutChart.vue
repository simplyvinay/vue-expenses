<template>
  <chart :options="options" :style="style" :theme="theme" />
</template>

<script>
export default {
  props: {
    showTitle: {
      type: Boolean,
      default: false
    },
    titleText: {
      type: String,
      default: ""
    },
    titleFontSize: {
      type: Number,
      default: 18
    },
    showLabel: {
      type: Boolean,
      default: false
    },
    showLabelLines: {
      type: Boolean,
      default: false
    },
    height: {
      type: Number,
      default: 100
    },
    theme: {
      type: String
    },
    showTooltip: {
      type: Boolean,
      default: true
    },
    pieRadiusInner: {
      type: Number,
      default: 45
    },
    pieRadiusOuter: {
      type: Number,
      default: 65
    },
    centerX: {
      type: Number,
      default: 50
    },
    centerY: {
      type: Number,
      default: 58
    },
    seriesData: {
      type: Array,
      default() {
        return [
          { value: 310, name: "Spent", itemStyle: { color: "#2779bd" } },
          { value: 234, name: "Remaining", itemStyle: { color: "#BDBDBD" } }
        ];
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
            radius: [this.pieRadiusInner + "%", this.pieRadiusOuter + "%"],
            center: [this.centerX + "%", this.centerY + "%"],
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
                show: this.showLabel,
                fontSize: "14",
                position: this.showLabel ? "outside" : "center"
              },
              emphasis: {
                show: !this.showLabel,
                textStyle: {
                  fontSize: "12",
                  fontWeight: "bold"
                },
                formatter: "{b}\n{c}\n({d}%)"
              }
            },
            labelLine: {
              normal: {
                show: this.showLabelLines
              }
            }
          }
        ],
        title: {
          text: this.titleText,
          x: "center",
          top: "10",
          show: this.showTitle,
          textStyle: {
            fontSize: this.titleFontSize,
            fontWeight: "normal"
          }
        },
        legend: {
          show: false,
          orient: "vertical",
          left: "left"
        },
        tooltip: {
          show: this.showTooltip,
          trigger: "item",
          formatter: "{b} : {c} <br/>({d}%)",
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