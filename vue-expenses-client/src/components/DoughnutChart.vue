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
    showLabelAndLines: {
      type: Boolean,
      default: false
    },
    height: {
      type: Number,
      default: 100
    }
  },
  data() {
    return {
      theme: this.$vuetify.theme.dark ? "dark" : "",
      options: {
        backgroundColor: this.$vuetify.theme.dark ? "#424242" : "",
        series: [
          {
            name: "Category",
            type: "pie",
            radius: ["45%", "65%"],
            center: ["50%", "60%"],
            data: [
              { value: 310, name: "Spent" },
              { value: 234, name: "Remaining" }
            ],
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
                show: this.showLabelAndLines,
                fontSize: "14"
              }
            },
            labelLine: {
              normal: {
                show: this.showLabelAndLines
              }
            }
          }
        ],
        color: ["#00c853", "#9575cd"],
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
          trigger: "item",
          formatter: "{a} <br/>{b} : {c} <br/>{d}%"
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