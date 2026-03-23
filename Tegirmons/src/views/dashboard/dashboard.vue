<template>
  <div class="dashboard-page">
    <div class="dashboard-header">
      <div>
        <h5 class="m-0">Dashboard</h5>
        <small class="text-muted">Tezkor ko'rsatkichlar</small>
      </div>
      <div class="d-flex align-items-center gap-2">
        <input
          type="date"
          class="form-control dashboard-date"
          v-model="filters.begin_date"
          @change="reload"
        />
        <input
          type="date"
          class="form-control dashboard-date"
          v-model="filters.end_date"
          @change="reload"
        />
        <button class="btn btn-success dashboard-btn" @click="reload">
          Yangilash
        </button>
      </div>
    </div>

    <!-- Bug'doy statistikasi -->
    <div class="card mt-3">
      <div class="card-body">
        <h6 class="mb-3">Bug'doy statistikasi ({{ filters.begin_date }} - {{ filters.end_date }})</h6>
        <div class="row">
          <div class="col-md-3 col-sm-6 mb-3" v-for="(card, idx) in bugdoyCards" :key="idx">
            <div class="stat-card" :style="{ background: card.bg }">
              <div class="stat-title">{{ card.title }}</div>
              <div class="stat-value">{{ card.value }}</div>
              <div class="stat-sub">{{ card.subtitle }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-md-3 col-sm-6 mb-3" v-for="(card, idx) in summaryCards" :key="idx">
        <div class="stat-card" :style="{ background: card.bg }">
          <div class="stat-title">{{ card.title }}</div>
          <div class="stat-value">{{ card.value }}</div>
          <div class="stat-sub">{{ card.subtitle }}</div>
        </div>
      </div>
    </div>

    <div class="card chart-card mt-2">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-2">
          <h6 class="m-0">Trend</h6>
          <span class="text-muted small">Keyinroq ma'lumotlar qo'shiladi</span>
        </div>
        <div class="chart-placeholder">
          <span v-if="loading">Yuklanmoqda...</span>
          <span v-else>Grafik ma'lumotlari hozircha yo'q</span>
        </div>
      </div>
    </div>

    <div class="card mt-2">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-2">
          <h6 class="m-0">Jadval</h6>
          <span class="text-muted small">Ma'lumot keyin qo'shiladi</span>
        </div>
        <div class="table-empty">Hozircha ma'lumot yo'q</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'dashboard',
  data() {
    const today = new Date().toISOString().slice(0, 10);
    return {
      loading: false,
      summary: {
        total_sales: 0,
        total_income: 0,
        total_expense: 0,
        total_clients: 0,
      },
      bugdoyStats: {
        total_bugdoy_received: 0,
        total_changed_to_product: 0,
        total_sold_for_money: 0,
        total_sold_for_money_summ: 0,
        total_to_zaxira: 0,
      },
      filters: {
        begin_date: today,
        end_date: today,
      },
    };
  },
  computed: {
    bugdoyCards() {
      return [
        {
          title: "Olib kelindi",
          value: this.formatQty(this.bugdoyStats.total_bugdoy_received),
          subtitle: "кг",
          bg: "linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%)",
        },
        {
          title: "Mahsulotga almashtirildi",
          value: this.formatQty(this.bugdoyStats.total_changed_to_product),
          subtitle: "кг",
          bg: "linear-gradient(90deg, #42e695 0%, #3bb2b8 100%)",
        },
        {
          title: "Pulga sotib olindi",
          value: this.formatQty(this.bugdoyStats.total_sold_for_money),
          subtitle: "кг / " + this.formatNumber(this.bugdoyStats.total_sold_for_money_summ) + " so'm",
          bg: "linear-gradient(90deg, #f89b29 0%, #ff0f7b 100%)",
        },
        {
          title: "Zaxiradga quyildi",
          value: this.formatQty(this.bugdoyStats.total_to_zaxira),
          subtitle: "кг",
          bg: "linear-gradient(90deg, #89f7fe 0%, #66a6ff 100%)",
        },
      ];
    },
    summaryCards() {
      return [
        {
          title: "Sotuvlar",
          value: this.formatNumber(this.summary.total_sales),
          subtitle: "so'm",
          bg: "linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%)",
        },
        {
          title: "Kirim",
          value: this.formatNumber(this.summary.total_income),
          subtitle: "so'm",
          bg: "linear-gradient(90deg, #42e695 0%, #3bb2b8 100%)",
        },
        {
          title: "Chiqim",
          value: this.formatNumber(this.summary.total_expense),
          subtitle: "so'm",
          bg: "linear-gradient(90deg, #f89b29 0%, #ff0f7b 100%)",
        },
        {
          title: "Mijozlar",
          value: this.formatNumber(this.summary.total_clients),
          subtitle: "ta",
          bg: "linear-gradient(90deg, #89f7fe 0%, #66a6ff 100%)",
        },
      ];
    },
  },
  async mounted() {
    await this.reload();
  },
  methods: {
    formatNumber(value) {
      if (value === null || value === undefined) return '0';
      const num = Number(value) || 0;
      return num.toLocaleString('ru-RU');
    },
    formatQty(value) {
      if (value === null || value === undefined) return '0';
      const num = parseFloat(value) || 0;
      // Agar butun son bo'lsa
      if (num % 1 === 0) {
        return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
      } else {
        // Kasr son bo'lsa, 2 ta onlik raqamgacha kesamiz
        let fixedNum = num.toFixed(2);
        // Oxiridagi nollarni olib tashlaymiz (masalan, 1.50 -> 1.5)
        fixedNum = fixedNum.replace(/\.?0+$/, '');
        return fixedNum.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
      }
    },
    async reload() {
      this.loading = true;
      try {
        // Bug'doy statistikasini olish
        const bugdoyParams = `?begin_date=${this.filters.begin_date}&end_date=${this.filters.end_date}`;
        const bugdoyUrl = this.$store.state.hostname + `/TegirmonTortilganBugdoyRoyxatiGroupDetails/getTodayBugdoyStatistics${bugdoyParams}`;
        console.log('Bugdoy API URL:', bugdoyUrl);
        
        const bugdoyRes = await fetch(bugdoyUrl);
        console.log('bugdoyRes status:', bugdoyRes.status);
        console.log('bugdoyRes Content-Type:', bugdoyRes.headers.get('content-type'));

        // Response ni clone qilamiz, chunki bir marta o'qiladi
        const clonedResponse = bugdoyRes.clone();

        if (bugdoyRes.status === 200 || bugdoyRes.status === 201) {
          const contentType = bugdoyRes.headers.get('content-type');
          if (contentType && contentType.includes('application/json')) {
            try {
              const bugdoyData = await bugdoyRes.json();
              console.log('bugdoyData:', bugdoyData);
              this.bugdoyStats = {
                total_bugdoy_received: bugdoyData.total_bugdoy_received || 0,
                total_changed_to_product: bugdoyData.total_changed_to_product || 0,
                total_sold_for_money: bugdoyData.total_sold_for_money || 0,
                total_sold_for_money_summ: bugdoyData.total_sold_for_money_summ || 0,
                total_to_zaxira: bugdoyData.total_to_zaxira || 0,
              };
            } catch (jsonError) {
              console.error('JSON parse error:', jsonError);
              const textResponse = await clonedResponse.text();
              console.error('Response text (first 500 chars):', textResponse.substring(0, 500));
            }
          } else {
            // HTML yoki boshqa format qaytarsa
            const textResponse = await clonedResponse.text();
            console.error('Response is not JSON. Content-Type:', contentType);
            console.error('Response text (first 500 chars):', textResponse.substring(0, 500));
          }
        } else {
          const errorText = await clonedResponse.text();
          console.error('API error response:', bugdoyRes.status, errorText.substring(0, 500));
        }

        // Boshqa statistikani olish
        const params = `?begin_date=${this.filters.begin_date}&end_date=${this.filters.end_date}`;
        const res = await fetch(this.$store.state.hostname + `/TegirmonDashboard/summary${params}`);
        if (res.status === 200 || res.status === 201) {
          const data = await res.json();
          this.summary = {
            total_sales: data.total_sales || 0,
            total_income: data.total_income || 0,
            total_expense: data.total_expense || 0,
            total_clients: data.total_clients || 0,
          };
        }
      } catch (e) {
        console.log(e);
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<style scoped lang="scss">
.dashboard-page {
  padding: 8px 10px 14px 10px;
  background: #f5f7fb;
  min-height: 100%;
}

.dashboard-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 14px 16px;
  border-radius: 10px;
  background: #ffffff;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
}

.dashboard-date {
  min-width: 160px;
  max-width: 200px;
  font-size: 12px;
}

.dashboard-btn {
  border-radius: 20px;
  padding: 6px 16px;
  font-size: 12px;
}

.stat-card {
  border-radius: 12px;
  color: #fff;
  padding: 14px 16px;
  box-shadow: rgba(15, 35, 52, 0.25) 0px 10px 24px -12px;
  height: 100%;
}

.stat-title {
  font-size: 12px;
  font-weight: 600;
  opacity: 0.9;
}

.stat-value {
  font-size: 20px;
  font-weight: 700;
  margin-top: 4px;
}

.stat-sub {
  font-size: 11px;
  margin-top: 2px;
  opacity: 0.9;
}

.chart-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
}

.chart-placeholder {
  height: 200px;
  border: 1px dashed #cfd8dc;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #78909c;
  font-size: 13px;
}

.table-empty {
  border: 1px dashed #cfd8dc;
  border-radius: 10px;
  padding: 18px;
  text-align: center;
  color: #78909c;
  font-size: 13px;
}
</style>

