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
            radius: "60%",
            center: ["50%", "60%"],
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
          text: "Expenses By Category",
          x: "center",
          top: "20",
          textStyle: {
            fontSize: 14,
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