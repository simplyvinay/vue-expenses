<script>
import { Doughnut } from "vue-chartjs";
export default {
  extends: Doughnut,
  props: ["data", "options"],
  mounted() {
    this.renderChart(
      {
        labels: ["Spent", "Limit"],
        datasets: [
          {
            label: "",
            backgroundColor: ["orange", "#EEEEEE"],
            data: [40, 20],
            borderWidth: 3
          }
        ]
      },
      {
        responsive: true,
        maintainAspectRatio: false,
        cutoutPercentage: 65,
        title: {
          display: true,
          text: "Budget",
          position: "top",
          fontFamily: "'Roboto', sans-serif",
          fontSize: 14,
          fontColor: "#424242",
          padding: 20,
        },
        legend: {
          display: false
        }
      }
    );
  }
};
</script>
