<template>
  <div>
    <pie-chart :chart-data="chartData" :options="chartOptions"></pie-chart>
  </div>
</template>

<script>
import { Pie } from "vue-chartjs";

export default {
  name: "TopProductsPieChart",
  components: {
    pieChart: {
      extends: Pie,
      props: ["chartData", "options"],
      mounted() {
        this.renderChart(this.chartData, this.options);
      }
    }
  },
  props: {
    products: {
      type: Array,
      required: true
    }
  },
  computed: {
    chartData() {
      return {
        labels: this.products.map(p => {
          if (!p.product_kg || p.product_kg <= 0) return p.product_name;

          const formattedQty = new Intl.NumberFormat('ru-RU').format(p.product_kg);
          return `${p.product_name} (${formattedQty} кг)`;
        }),
        datasets: [
          {
            label: "Sotilgan miqdor",
            data: this.products.map(p => p.qty),
            backgroundColor: [
              "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF"
            ]
          }
        ]
      };
    },
    chartOptions() {
      return {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
          position: "bottom"
        }
      };
    }
  }
};
</script>

<style scoped>
div {
  height: 270px; /* Pie chart balandligi */
}
</style>