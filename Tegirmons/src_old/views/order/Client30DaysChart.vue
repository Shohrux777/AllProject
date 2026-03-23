<template>
  <div class="client-chart-wrapper mt-2">
    <apexchart
      type="area"
      height="250"
      :options="chartOptions"
      :series="series"
    />
  </div>
</template>

<script>
import axios from "axios";
import ApexChart from "vue-apexcharts";

export default {
  name: "Client30DaysChart",
  components: { apexchart: ApexChart },
  props: {
    clientId: {
      type: Number,
      
      default: 0,
    }
  },
  data() {
    return {
      rawData: [],
      series: [{
        name: "To'lovlar",
        data: []
      }],
      chartOptions: {
        chart: {
          toolbar: { show: false },
          zoom: { enabled: false },
        },
        title: {
            text: 'Статистика платежей за последние 30 дней',
            align: 'left', // 'left' | 'center' | 'right'
            margin: 10,
            offsetX: 0,
            offsetY: 0,
            style: {
                fontSize: '16px',
                fontWeight: '600',
                fontFamily: 'inherit'
            }
            },
        dataLabels: { enabled: false },
        stroke: { curve: "smooth", width: 3 },
        fill: {
            type: "gradient",
            gradient: {
                shadeIntensity: 1,
                opacityFrom: 0.8,   // yuqoridan boshlanishdagi opacity
                opacityTo: 0.8,     // pastga tushmasin, barqaror qolsin
                stops: [0, 100]
            }
            },
        xaxis: {
          categories: [],
          labels: { rotate: -45 }
        },
        yaxis: {
        labels: {
            formatter: function (val) {
            if (val >= 1000000000) return (val / 1000000000).toFixed(1) + " B";
            if (val >= 1000000) return (val / 1000000).toFixed(1) + " M";
            if (val >= 1000) return (val / 1000).toFixed(1) + " K";
            return val;
            }
        }
        },
        grid: {
            padding: {
                top: -10,
                bottom: -10,
            }
        },

        tooltip: {
          y: {
            formatter: (val) => this.formatSumm(val)
          }
        }
      }
    };
  },
  mounted() {
    this.loadData();
  },
  methods: {
    async loadData() {
        if(this.clientId){
            const response = await axios.get(this.$store.state.hostname + 
                `/TegirmonOrderCheck/last30days/${this.clientId}`
            );
            console.log(response.data)
            this.rawData = response.data;
            this.prepareDailyData();
        }
      
    },

    prepareDailyData() {
    const labels = [];
    const data = [];

    this.rawData.forEach(item => {
        const dateObj = new Date(item.date);
        const day = String(dateObj.getDate()).padStart(2, "0");
        const month = String(dateObj.getMonth() + 1).padStart(2, "0");
        labels.push(`${day}.${month}`);
        data.push(item.total);
    });

    this.series = [{
        name: "To'lovlar",
        data: data
    }];

    this.chartOptions = {
        ...this.chartOptions,
        xaxis: {
        ...this.chartOptions.xaxis,
        categories: labels
        }
    };
    },

    formatSumm(val) {
      if (val >= 1_000_000) {
        return (val / 1_000_000).toFixed(1).replace(/\.0$/, '') + " M";
      }
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    }
  }
};
</script>

<style scoped>
.client-chart-wrapper {
  background: #ffffff;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
}
</style>
