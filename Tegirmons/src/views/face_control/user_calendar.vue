<template>
  <div>
    <label>Xodimni tanlang:</label>
    <select v-model="selectedUserId" @change="generateCalendar(selectedYear, selectedMonth)">
      <option v-for="user in users" :value="user.id" :key="user.id">
        {{ user.name }}
      </option>
    </select>
    <div class="calendar-controls">
        <select v-model="selectedYear" @change="refreshCalendar">
            <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
        </select>

        <select v-model="selectedMonth" @change="refreshCalendar">
            <option v-for="month in months" :key="month.value" :value="month.value">
            {{ month.name }}
            </option>
        </select>
    </div>

    <table border="1" class="calendar-table">
      <thead>
        <tr>
          <th v-for="day in days" :key="day">{{day}}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(week, index) in calendar" :key="index">
          <td v-for="day in week" :key="day.date.toDateString()" :class="{ today: day.isToday }">
            <div>{{ day.date.getDate() }}</div>
            <small v-if="day.hours">{{ day.hours }}</small>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
export default {
  data() {
    const now = new Date();
    return {
      selectedUserId: null,
      users: [{
        name: 'shohrux',
        id: 2
      }], // API dan user list
      workdays: 
        [
            { "k_date": "2025-06-01", "work_time": "04:30:00" },
            { "k_date": "2025-06-02", "work_time": "07:15:00" },
            { "k_date": "2025-06-04", "work_time": "08:00:00" }
        ], // API dan bir oyga qaytgan kunlik ishlaganlar
      calendar: [],
      selectedMonth: now.getMonth(), // 0-based
      selectedYear: now.getFullYear(), 
      months: [
        { value: 0, name: 'Yanvar' },
        { value: 1, name: 'Fevral' },
        { value: 2, name: 'Mart' },
        { value: 3, name: 'Aprel' },
        { value: 4, name: 'May' },
        { value: 5, name: 'Iyun' },
        { value: 6, name: 'Iyul' },
        { value: 7, name: 'Avgust' },
        { value: 8, name: 'Sentabr' },
        { value: 9, name: 'Oktabr' },
        { value: 10, name: 'Noyabr' },
        { value: 11, name: 'Dekabr' }
        ],
        days: ['Du', 'Se', 'Ch', 'Pa', 'Ju', 'Sh', 'Ya']
    };
  },
  computed: {
        years() {
            const currentYear = new Date().getFullYear();
            const startYear = currentYear - 5;
            return Array.from({ length: 50 }, (_, i) => startYear + i);
        }
    },
  methods: {
    async loadWorkDays() {
      const res = await fetch(`/api/worked-days?userid=${this.selectedUserId}&month=2025-06`);
      this.workdays = await res.json();
      this.generateCalendar();
    },
    refreshCalendar() {
        this.generateCalendar(this.selectedYear, this.selectedMonth);
    },
    generateCalendar(year, month) {
    const start = new Date(year, month, 1);
    const end = new Date(year, month + 1, 0);
    
    const weeks = [];
    let week = [];

    let date = new Date(start);
    date.setDate(date.getDate() - ((date.getDay() + 6) % 7)); // Dushanbadan boshlash

    const today = new Date();
    const todayStr = `${today.getFullYear()}-${(today.getMonth() + 1).toString().padStart(2, '0')}-${today.getDate().toString().padStart(2, '0')}`;

    while (date <= end || week.length > 0) {
        const isoDate = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
        const work = this.workdays.find(w => w.k_date === isoDate);

        week.push({
        date: new Date(date),
        isToday: isoDate === todayStr,
        hours: work ? work.work_time.slice(0, 5) : null
        });

        if (week.length === 7) {
        weeks.push(week);
        week = [];
        }

        date.setDate(date.getDate() + 1);
    }

    this.calendar = weeks;
    }
  },
  mounted() {
    // User listni yuklash
    // fetch('/api/user-list')
    //   .then(res => res.json())
    //   .then(data => (this.users = data));
    this.refreshCalendar();
  }
};
</script>
<style scoped>
.calendar-table {
  width: 100%;
  text-align: center;
}
td {
  height: 70px;
  vertical-align: top;
}
td:hover{
    background: blue;
}
tr:hover{
    background: white !important;
}
.today {
  background-color: #d1f0d1;
}
</style>