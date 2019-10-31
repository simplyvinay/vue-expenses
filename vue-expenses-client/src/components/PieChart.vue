<script>
import { Pie } from "vue-chartjs";

export default {
  extends: Pie,
  props: ["options"],
  mounted() {
    this.renderChart(
      {
        labels: ["Green", "Red", "Blue"],
        datasets: [
          {
            label: "",
            backgroundColor: ["#41B883", "#E46651", "#00D8FF"],
            data: [1, 10, 5]
          }
        ]
      },
      {
        responsive: true,
        maintainAspectRatio: false,
        title: {
          display: true,
          fontFamily: "'Roboto', sans-serif",
          fontSize: 14,
          fontColor: "#424242",
          text: "Budget",
        },
        legend: {
          display: false
        }
      }
    );
  }
};
</script>
