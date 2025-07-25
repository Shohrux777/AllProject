<template>
  <div style="width: 350px;">
    <div class="calendar-controls">
        <select v-model="selectedYear" @change="refreshCalendar" >
            <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
        </select>

        <select v-model="selectedMonth" @change="refreshCalendar">
            <option v-for="month in months" :key="month.value" :value="month.value">
            {{ month.name }}
            </option>
        </select>
    </div>

    <table  class="calendar-table">
      <thead>
        <tr class="border-bottom">
          <th v-for="day in days" :key="day">{{day}}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(week, index) in calendar" :key="index" class="border-bottom">
          <td v-for="day in week" :key="day.date.toDateString()" @click="chooseDayInfo(day)" 
            :class="{
                    'today': day.isToday,
                    'working': day.hours,
                    'other-month': day.isOtherMonth
                    }">
            <div class="calendar_date">{{ day.date.getDate() }}</div>
            <small class="calendar_hours" v-if="day.hours">{{ day.hours }}</small>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import user from '../../store/modules/user';

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
        [], // API dan bir oyga qaytgan kunlik ishlaganlar
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
        days: ['Du', 'Se', 'Ch', 'Pa', 'Ju', 'Sh', 'Ya'],
        user_id: 0,
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
    async chooseDayInfo(day){
      console.log(day);
      this.$emit('choose_day', day)
    },
    async update_user_salary(user_id){
      console.log(user_id)
      this.user_id = user_id;
      const selectedDate = `${this.selectedYear}-${(this.selectedMonth + 1).toString().padStart(2, '0')}-01`;
      localStorage.selectedDate = selectedDate;
      console.log('selectedDate', selectedDate)
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganVaqt/getUserWorkedDays?page=0&size=200&userid=" + user_id + '&month=' + selectedDate);
        const data = await response.json();
        console.log('K_data',data)
        if(response.status == 200 || response.status == 201){
          this.workdays = data.items_list;
        }
        else{
          this.workdays = [];
        }
        this.generateCalendar(this.selectedYear, this.selectedMonth);
      }
      catch(error){
        console.log(error)
      }
    },
    async loadWorkDays() {
      const res = await fetch(`/api/worked-days?userid=${this.selectedUserId}&month=2025-06`);
      this.workdays = await res.json();
      this.generateCalendar();
    },
    async refreshCalendar() {
        if(this.user_id){
          await this.update_user_salary(this.user_id);
        }
        else{
          this.generateCalendar(this.selectedYear, this.selectedMonth);
        }
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
        const work = null;
        if(this.workdays.length>0){
          work = this.workdays.find(w => w.K_date.slice(0, 10) === isoDate);
        }

        week.push({
        date: new Date(date),
        isToday: isoDate === todayStr,
        hours: work ? work.work_time.slice(0, 5) : null,
        isOtherMonth: date.getMonth() !== month
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
  /* width: 100%; */
  text-align: center;
}
td {
  vertical-align: top;
}
td:hover{
    background: rgb(33, 255, 100);
    cursor: pointer;
}
tr:hover{
    background: white !important;
}
.today {
  background-color: #c7eefd;
}
.working{
  background-color: #b8fdc1;
}
.calendar-table td {
  width: 50px !important;
  height: 50px !important;
  vertical-align: top;
  padding: 5px;
  box-sizing: border-box;
  position: relative;
}
.calendar-table thead tr th{
  font-weight: 600 !important;
}
.other-month {
  color: #aaa; /* ochroq ko‘rinish uchun */
  background-color: #f9f9f9; /* yoki faqat rangni o‘zgartiring */
}
.calendar-controls{
  width: 100%;
  display: flex;
  justify-content: flex-start;
  padding: 5px 0;
}
.calendar-controls select{
  width: 120px;
  height: 30px;
  border: 1px solid rgb(204, 204, 204);
  border-radius: 5px;
  color: rgb(58, 57, 57);
  font-size: 14px;
  padding: 5px;
  margin-left: 5px;
}
</style>