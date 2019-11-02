<template>
  <chart :options="options" style="width: 100%" :theme="theme" />
</template>

<script>
export default {
   props: {
    theme: {
      type: String     
    }
  },
  data() {
    return {
      options: {
        backgroundColor: this.$vuetify.theme.dark ? "#424242" : "",
        grid: {
          show: false,
          top: 80,
          bottom: 80,
        },
        xAxis: {
          data: [
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"
          ],
          axisLabel: {
            textStyle: {
              //color: "#616161"
            }
          }
        },
        yAxis: [
          {
            type: "value",
            splitLine: {
              show: false
            },
            axisLabel: {
              textStyle: {
                //color: "#616161"
              }
            }
          }
        ],
        series: [
          {
            type: "bar",
            data: [630, 750, 240, 800, 630, 750, 240, 920, 63, 750, 240, 780],
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
            //color: "#616161",
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