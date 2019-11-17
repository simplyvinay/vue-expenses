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
    seriesData: {
      type: Object,
      default: function() {
        return [
          { value: 310, name: "Spent" },
          { value: 234, name: "Remaining" }
        ];
      }
    },
    seriesAColour: {
      type: String,
      default: "#2779bd"
    },
    seriesBColour: {
      type: String,
      default: "#BDBDBD"
    }
  },
  data() {
    return {
      options: {
        backgroundColor: this.$vuetify.theme.dark ? "#424242" : "",
        textStyle: {
          fontFamily: "Nunito"
        },
        series: [
          {
            name: "Category",
            type: "pie",
            radius: ["45%", "65%"],
            center: ["50%", "60%"],
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
                formatter: "{b}\n({d}%)"
              }
            },
            labelLine: {
              normal: {
                show: this.showLabelLines
              }
            }
          }
        ],
        color: [this.seriesAColour, this.seriesBColour],
        title: {
          text: this.titleText,
          //subtext: "Per Month",
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
          formatter: "{a} <br/>{b} : {c} <br/>({d}%)",
          textStyle: {
            fontSize: 12,
            fontWeight: "normal"
          }
        }
      },
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