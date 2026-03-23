<template>
  <div>
    <apexchart type="bar" height="350" :options="chartOptions" :series="series" />
  </div>
</template>

<script>
import VueApexCharts from "vue-apexcharts";

export default {
  name: "weeklySkladChart",
  components: {
    apexchart: VueApexCharts,
  },
  props: {
    stats: {
      type: Array,
      required: true,
      default: () => []
    }
  },
  computed: {
    chartOptions() {
      return {
        chart: {
          stacked: true,
          toolbar: { show: true }
        },
        colors: ["#28a745", "#dc3545"],
        xaxis: {
          categories: this.stats.map(s => s.date) // backenddan keladigan kunlar
        },
        
        plotOptions: {
            bar: {
            columnWidth: "40%", // ustun qalinligini kamaytirdik
            borderRadius: 3
            }
        },
        dataLabels: {
          enabled: true,
          formatter: function (val) {
            return val.toLocaleString(); // 10000 => "10,000"
          },
          style: {
            fontSize: "12px",
            colors: ["#333"]
          }
        },
        yaxis: {
          labels: {
            formatter: function (val) {
              return val.toLocaleString(); // o‘qishli qilib chiqaradi
            }
          }
        },
        legend: {
          position: "top",
        }
      };
    },
    series() {
      return [
        {
          name: "Сумма приходов",
          data: this.stats.map(s => s.kirimSum)
        },
        {
          name: "Сумма расходов",
          data: this.stats.map(s => s.chiqimSum)
        }
      ];
    }
  }
};
</script>
<style scoped>
.apexcharts-series path {
  cursor: pointer;
}
.apexcharts-bar-series path {
  cursor: pointer;
}
.apexcharts-bar-series path:hover {
  opacity: 0.8;
  cursor: pointer;
}
</style>