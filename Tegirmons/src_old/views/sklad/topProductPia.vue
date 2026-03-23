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
          const kg = p.product_kg || 0;
          const qty = p.qty || 0;

          // Format numbers with space as thousands separator
          const formattedKg = new Intl.NumberFormat('ru-RU').format(kg);
          const formattedQty = new Intl.NumberFormat('ru-RU').format(qty);

          // Mahsulot nomi + kg va sht ko'rinishida
          return `${p.product_name} (${formattedKg} кг / ${formattedQty} шт)`;
        }),
        datasets: [
          {
            // Sektorlarda kg bo'yicha ulush
            label: "Sarflangan mahsulot (kg)",
            data: this.products.map(p => p.product_kg || 0),
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
        },
        tooltips: {
          callbacks: {
            // Tooltip ichida ham kg va qty ni alohida ko'rsatib beramiz
            label: (tooltipItem, data) => {
              const index = tooltipItem.index;
              const p = this.products[index] || {};
              const kg = p.product_kg || 0;
              const qty = p.qty || 0;

              const formattedKg = new Intl.NumberFormat('ru-RU').format(kg);
              const formattedQty = new Intl.NumberFormat('ru-RU').format(qty);

              return `${p.product_name}: ${formattedKg} кг, ${formattedQty} шт`;
            }
          }
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