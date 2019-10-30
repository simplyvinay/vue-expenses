<script>
import { Bar } from "vue-chartjs";

export default {
  extends: Bar,
  props: ["options"],
  mounted() {
    this.renderChart(
      {
        labels: [
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
        datasets: [
          {
            label: "",
            backgroundColor: "#0288D1",
            data: [1, 20, 12, 32, 10, 40, 30, 39, 28, 20, 12, 11]            
          }
        ]
      },
      {
        responsive: true,
        maintainAspectRatio: false,
        title: {
          display: true,
          text: "Expenses",
          position: "top",
          fontFamily: "'Roboto', sans-serif",
          fontSize: 14,
          fontColor: "#424242",
          padding: 20          
        },
        legend: {
          display: false
        },
        scales: {
          xAxes: [
            {
              display: true,
              barPercentage: 0.4,
              gridLines: {
                display: false,
              }
            }
          ],
          yAxes: [
            {
              display: true,
              gridLines: {
                display: false,
              }
            }
          ]
        }
      }
    );
  }
};
</script>
