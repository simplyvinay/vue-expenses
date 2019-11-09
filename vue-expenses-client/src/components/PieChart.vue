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
        textStyle: {
          fontFamily: 'Nunito'
        },
        series: [
          {
            name: "Category",
            type: "pie",
            radius: "60%",
            center: ["50%", "50%"],
            roseType: "radius",
            data: [
              { value: 335, name: "General\nExpenses" },
              { value: 310, name: "Shopping" },
              { value: 234, name: "Utilities" },
              { value: 210, name: "Groceries" },
              { value: 400, name: "Travel" },
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
                //position: "inner",
                show: true,
                //fontWeight: "bolder",
                fontSize: "14"
              }
            },
            labelLine: {
              normal: {
                show: true
              }
            }
          }
        ],
        title: {
          text: "Expenses By Category",
          //subtext: "All Time",
          x: "center",
          top: "20",
          textStyle: {
            //color: "#616161",
            fontSize: 14,
            fontWeight: 'normal'
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
      }
    };
  }
};
</script>

<style>
</style>